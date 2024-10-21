using DG.Tweening;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    public Transform laserOrigin;  // �������� �߻�Ǵ� ��ġ
    public LineRenderer laserLineRenderer;  // �������� �ð������� ǥ���� LineRenderer
    public float maxLaserDistance = 100f;  // �������� �ִ� �Ÿ�
    public LayerMask reflectableLayers;  // �������� �ݻ�� �� �ִ� ���̾� (�ſ��̳� ��ǥ ��)
    public Transform doorLeft;  // ���� �� Transform
    public Transform doorRight;  // ������ �� Transform

    public float leftStartPosZ;  // ���� ���� ���� Z ��ǥ
    public float rightStartPosZ;  // ������ ���� ���� Z ��ǥ
    public float endPosZ = 3f;  // ���� ������ ���� �̵� �Ÿ�
    public float duration = 1f;  // ���� ������ ������ �ִϸ��̼� ���� �ð�
    public Ease motionEase = Ease.OutQuad;  // DoTween�� �ִϸ��̼� ��¡ ����
    private bool isDoorOpen = false;  // ���� ���� �ִ��� ���θ� ����
    private bool isAnimating = false;  // ���� �ִϸ��̼��� ���� ������ ���θ� ����
    private bool targetHit = false; // �������� ��ǥ�� ��Ҵ��� ����

    private void Start()
    {
        // laserOrigin�� null�� ���, �⺻������ �� ������Ʈ�� Transform�� ���
        if (laserOrigin == null)
        {
            laserOrigin = transform;
        }
        // ���� �� ������ Z ��ǥ�� ����
        leftStartPosZ = doorLeft.localPosition.z;
        rightStartPosZ = doorRight.localPosition.z;
    }

    private void Update()
    {
        FireLaser();
    }

    void FireLaser()
    {
        Vector3 laserDirection = laserOrigin.forward;  // ������ �߻� ����
        Vector3 laserPosition = laserOrigin.position;  // ������ ���� ��ġ
        laserLineRenderer.positionCount = 1;  // LineRenderer�� �������� ������ ���� ��ġ�� ����
        laserLineRenderer.SetPosition(0, laserPosition);

        bool isReflecting = true;
        int reflections = 0;
        float remainingDistance = maxLaserDistance;
        bool hitTarget = false;  // ��ǥ�� ��Ҵ��� Ȯ���ϴ� �÷���

        while (isReflecting && reflections < 10)  // �ִ� 10������ �ݻ�
        {
            // LayerMask ������ Raycast
            if (Physics.Raycast(laserPosition, laserDirection, out RaycastHit hit, remainingDistance, reflectableLayers))
            {
                laserLineRenderer.positionCount += 1;
                laserLineRenderer.SetPosition(laserLineRenderer.positionCount - 1, hit.point);

                // �������� �ſ￡ �¾��� ��� �ݻ�
                if (hit.collider.CompareTag("Mirror"))
                {
                    Vector3 incomingDirection = laserDirection;  // �������� �Ի簢
                    Vector3 normal = hit.normal;  // �ſ� ǥ���� ���� ����
                    laserDirection = Vector3.Reflect(incomingDirection, normal);  // �ݻ簢 ���

                    laserPosition = hit.point;
                    reflections++;
                    remainingDistance -= hit.distance;
                }
                else if (hit.collider.CompareTag("Target"))  // ��ǥ ������ �¾��� ���
                {
                    hitTarget = true;  // ��ǥ�� ��Ҵٴ� ���� ���
                    if (!isDoorOpen)
                    {
                        CompletePuzzle();  // ���� �Ϸ� ó�� �� �� ����
                    }
                    isReflecting = false;
                }
                else
                {
                    isReflecting = false;
                }
            }
            else
            {
                isReflecting = false;
                laserLineRenderer.positionCount += 1;
                laserLineRenderer.SetPosition(laserLineRenderer.positionCount - 1, laserPosition + laserDirection * remainingDistance);
            }
        }

        // ��ǥ�� ���� �ʾ��� ��� ���� ����
        if (!hitTarget && isDoorOpen)
        {
            CloseDoor();
        }
    }

    void CompletePuzzle()
    {
        isAnimating = true;  // �ִϸ��̼��� ���� ������ ǥ��
        DOTween.Sequence()
            .Append(doorLeft.DOLocalMoveZ(leftStartPosZ + endPosZ, duration))  // ���� ���� ����
            .Join(doorRight.DOLocalMoveZ(rightStartPosZ - endPosZ, duration))  // ������ ���� ����
            .SetEase(motionEase)  // ��¡ ����
            .OnComplete(() =>
            {
                isAnimating = false;  // �ִϸ��̼� �Ϸ�
                isDoorOpen = true;  // ���� ������
            });
    }

    // ���� �ݴ� �޼���
    void CloseDoor()
    {
        isAnimating = true;  // �ִϸ��̼��� ���� ������ ǥ��
        DOTween.Sequence()
            .Append(doorLeft.DOLocalMoveZ(leftStartPosZ, duration))  // ���� ���� �ݱ�
            .Join(doorRight.DOLocalMoveZ(rightStartPosZ, duration))  // ������ ���� �ݱ�
            .SetEase(motionEase)  // ��¡ ����
            .OnComplete(() =>
            {
                isAnimating = false;  // �ִϸ��̼� �Ϸ�
                isDoorOpen = false;  // ���� ������
            });
    }
}
