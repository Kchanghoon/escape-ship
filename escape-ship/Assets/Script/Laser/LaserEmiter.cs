using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    public Transform laserOrigin;  // �������� �߻�Ǵ� ��ġ
    public LineRenderer laserLineRenderer;  // �������� �ð������� ǥ���� LineRenderer
    public float maxLaserDistance = 100f;  // �������� �ִ� �Ÿ�
    public LayerMask reflectableLayers;  // �������� �ݻ�� �� �ִ� ���̾� (�ſ��̳� ��ǥ ��)

    private void Start()
    {
        // laserOrigin�� null�� ���, �⺻������ �� ������Ʈ�� Transform�� ���
        if (laserOrigin == null)
        {
            laserOrigin = transform;
        }
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
                    Debug.Log("�������� ��ǥ�� �����߽��ϴ�!");
                    CompletePuzzle();  // ���� �Ϸ� ó��
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
    }

    void CompletePuzzle()
    {
        // ���� �Ϸ� ó��
        Debug.Log("������ �Ϸ�Ǿ����ϴ�!");
    }
}
