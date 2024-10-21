using UnityEngine;
using DG.Tweening;  // DoTween ���ӽ����̽� �߰�

public class MirrorItem : MonoBehaviour
{
    public GameObject mirror;  // ù ��° ȸ����ų �ſ� ������Ʈ
    public GameObject mirror2;  // �� ��° ȸ����ų �ſ� ������Ʈ
    public float rotationAmount = 45f;  // �� ���� ȸ���� ����
    public float rotationDuration = 0.5f;  // ȸ�� �ð� (DoTween���� ����� �ִϸ��̼� �ð�)
    private bool isMouseOver = false;  // ���콺�� ��ư ���� �ִ��� ����
    private bool isAnimating = false;  // �ִϸ��̼��� ���� ������ ����

    public void Start()
    {
        // Ű �Ŵ������� Ư�� Ű �׼ǿ� ���� �г��� ���� ����� �Ҵ�
        KeyManager.Instance.keyDic[KeyAction.PickUp] += StartRotation;
    }

    private void OnMouseEnter()
    {
        // ���콺�� ��ư ������Ʈ ���� ������ �� ȣ��
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        // ���콺�� ��ư ������Ʈ���� ������ �� ȣ��
        isMouseOver = false;
    }

    void StartRotation()
    {
        // �ִϸ��̼��� ���� ���� �ƴϰ�, ���콺�� ������Ʈ ���� ���� ���� ����
        if (isMouseOver && !isAnimating)
        {
            if (mirror != null && mirror2 != null )
            {
                // �ִϸ��̼��� ���۵Ǿ����� ǥ��
                isAnimating = true;

                // ��ǥ ȸ�� ���� ����
                float targetRotationY1 = mirror.transform.localEulerAngles.y + rotationAmount;
                float targetRotationY2 = mirror2.transform.localEulerAngles.y + rotationAmount;

                // DoTween�� ����Ͽ� �� �ſ��� ���� �ε巴�� ȸ��
                mirror.transform.DORotate(new Vector3(0, targetRotationY1, 0), rotationDuration)
                    .SetEase(Ease.OutQuad);  // �ڿ������� ����

                mirror2.transform.DORotate(new Vector3(0, targetRotationY2, 0), rotationDuration)
                    .SetEase(Ease.OutQuad).OnComplete(() => isAnimating = false);  // ������ �ſ� �ִϸ��̼��� �Ϸ�� �� �ִϸ��̼� ���� ����;  // �ڿ������� ����

            }
            else
            {
                Debug.LogWarning("Mirror ������Ʈ�� �Ҵ���� �ʾҽ��ϴ�!");
            }
        }
    }
}
