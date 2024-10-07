using UnityEngine;
using DG.Tweening;  // DoTween ���ӽ����̽� �߰�

public class MirrorItem : MonoBehaviour
{
    public GameObject mirror;  // ȸ����ų �ſ� ������Ʈ
    public GameObject mirror2;  // ȸ����ų �� ��° �ſ� ������Ʈ
    public float rotationAmount = 45f;  // �� ���� ȸ���� ����
    public float rotationDuration = 0.5f;  // ȸ�� �ð� (DoTween���� ����� �ִϸ��̼� �ð�)
    public KeyCode rotateKey = KeyCode.X;  // ȸ����ų �� ���� Ű
    private bool isMouseOver = false;  // ���콺�� ��ư ���� �ִ��� ����

    private void Update()
    {
        // ���콺�� ��ư ���� ���� ��, ������ Ű�� ������ ���� �ſ��� ȸ��
        if (isMouseOver && Input.GetKeyDown(rotateKey))
        {
            StartRotation();
        }
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
        if (mirror != null && mirror2 != null)
        {
            // ��ǥ ȸ�� ���� ����
            float targetRotationY1 = mirror.transform.eulerAngles.y + rotationAmount;
            float targetRotationY2 = mirror2.transform.eulerAngles.y + rotationAmount;

            // DoTween�� ����Ͽ� �� �ſ��� ���� �ε巴�� ȸ��
            mirror.transform.DORotate(new Vector3(0, targetRotationY1, 0), rotationDuration)
                .SetEase(Ease.OutQuad);  // �ڿ������� ����

            mirror2.transform.DORotate(new Vector3(0, targetRotationY2, 0), rotationDuration)
                .SetEase(Ease.OutQuad);  // �ڿ������� ����
        }
        else
        {
            Debug.LogWarning("Mirror ������Ʈ�� �Ҵ���� �ʾҽ��ϴ�!");
        }
    }
}
