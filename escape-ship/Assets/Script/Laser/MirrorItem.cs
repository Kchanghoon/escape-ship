using UnityEngine;

public class MirrorItem : MonoBehaviour
{
    public GameObject mirror;  // ȸ����ų �ſ� ������Ʈ
    public GameObject mirror2;  // ȸ����ų �ſ� ������Ʈ
    public float rotationAmount = 45f;  // �� ���� ȸ���� ����
    public float rotationSpeed = 50f;  // ȸ�� �ӵ�
    public KeyCode rotateKey = KeyCode.X;  // ȸ����ų �� ���� Ű
    private bool isRotating = false;  // ȸ�� ������ ����
    private float targetRotationY;  // ��ǥ Y�� ȸ������
    private bool isMouseOver = false;  // ���콺�� ��ư ���� �ִ��� ����

    private void Update()
    {
        // ���콺�� ��ư ���� ���� ��, ������ Ű�� ������ ���� �ſ��� ȸ��
        if (isMouseOver && Input.GetKeyDown(rotateKey) && !isRotating)
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
        if (mirror != null)
        {
            isRotating = true;  // ȸ�� ����
            targetRotationY = mirror.transform.eulerAngles.y + rotationAmount;  // �ſ��� ��ǥ ȸ���� ����
            targetRotationY = mirror2.transform.eulerAngles.y + rotationAmount;  // �ſ��� ��ǥ ȸ���� ����
        }
        else
        {
            Debug.LogWarning("Mirror ������Ʈ�� �Ҵ���� �ʾҽ��ϴ�!");
        }
    }

    void ContinueRotation()
    {
        // �ε巴�� ��ǥ ������ �ſ��� ȸ��
        if (mirror != null)
        {
            float newYRotation = Mathf.MoveTowardsAngle(mirror.transform.eulerAngles.y, targetRotationY, rotationSpeed * Time.deltaTime);
            mirror.transform.eulerAngles = new Vector3(mirror.transform.eulerAngles.x, newYRotation, mirror.transform.eulerAngles.z);
            mirror2.transform.eulerAngles = new Vector3(mirror.transform.eulerAngles.x, newYRotation, mirror.transform.eulerAngles.z);

            // ��ǥ ������ �����ϸ� ȸ�� ����
            if (Mathf.Abs(newYRotation - targetRotationY) < 0.01f)
            {
                isRotating = false;
            }
        }
    }
}
