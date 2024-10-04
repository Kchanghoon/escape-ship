using UnityEngine;

public class MirrorItem : MonoBehaviour
{
    public float rotationAmount = 45f;  // �� ���� ȸ���� ����
    public float rotationSpeed = 50f;  // ȸ�� �ӵ�
    private bool isRotating = false;  // ȸ�� ������ ����
    private float targetRotationY;  // ��ǥ Y�� ȸ������
    private bool isMouseOver = false;  // ���콺�� ������Ʈ ���� �ִ��� ����

    private void Update()
    {
        // ���콺�� ������Ʈ ���� ���� �� XŰ�� ������ ���� 45���� ȸ��
        if (isMouseOver && Input.GetKeyDown(KeyCode.X) && !isRotating)
        {
            StartRotation();
        }

        if (isRotating)
        {
            ContinueRotation();
        }
    }

    private void OnMouseEnter()
    {
        // ���콺�� ������Ʈ ���� ������ �� ȣ��
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        // ���콺�� ������Ʈ���� ������ �� ȣ��
        isMouseOver = false;
    }

    void StartRotation()
    {
        isRotating = true;  // ȸ�� ����
        targetRotationY = transform.eulerAngles.y + rotationAmount;  // ��ǥ ȸ���� ����
    }

    void ContinueRotation()
    {
        // �ε巴�� ��ǥ ������ ȸ��
        float newYRotation = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotationY, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newYRotation, transform.eulerAngles.z);

        // ��ǥ ������ �����ϸ� ȸ�� ����
        if (Mathf.Abs(newYRotation - targetRotationY) < 0.01f)
        {
            isRotating = false;
        }
    }
}
