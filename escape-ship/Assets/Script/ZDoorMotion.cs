using UnityEngine;
using DG.Tweening;

public class ZDoorMotion : MonoBehaviour
{
    public Transform door;  // ���Ϸ� �����̴� ��
    public float startPosY;  // �� ���� ��ġ Y
    public float endPosY = 3f;  // ���� ���� �� �̵��� �Ÿ�
    public float duration = 1f;  // ���� ������ �� �ɸ��� �ð�
    public Ease motionEase = Ease.OutQuad;  // �ִϸ��̼� �ӵ� ����

    private bool isDoorOpen = false;  // ���� ���� �ִ��� ���θ� �����ϴ� ����
    private bool isAnimating = false;  // �ִϸ��̼��� ���� ������ Ȯ���ϴ� �÷���

    void Start()
    {
        // ���� ��ġ ����
        startPosY = door.localPosition.y;
    }

    // ���� ���� �Լ�
    public void OpenDoor()
    {
        if (isAnimating || isDoorOpen) return;  // �̹� �ִϸ��̼� ���̰ų� ���� ���� ������ �ƹ� �۾��� ���� ����
        isAnimating = true;  // �ִϸ��̼��� ���� ������ ���
        door.DOLocalMoveY(startPosY + endPosY, duration)  // ���� ���� �̵�
            .SetEase(motionEase)
            .OnComplete(() =>
            {
                isAnimating = false;  // �ִϸ��̼��� �������� ���
                isDoorOpen = true;  // ���� ���� ���·� ���
            });
    }

    // ���� �ݴ� �Լ�
    public void CloseDoor()
    {
        if (isAnimating || !isDoorOpen) return;  // �̹� �ִϸ��̼� ���̰ų� ���� ���� ������ �ƹ� �۾��� ���� ����
        isAnimating = true;  // �ִϸ��̼��� ���� ������ ���
        door.DOLocalMoveY(startPosY, duration)  // ���� ���� ��ġ�� �̵�
            .SetEase(motionEase)
            .OnComplete(() =>
            {
                isAnimating = false;  // �ִϸ��̼��� �������� ���
                isDoorOpen = false;  // ���� ���� ���·� ���
            });
    }
}
