using UnityEngine;

public class MirrorItem : MonoBehaviour
{
    public float rotationAmount = 45f;  // 한 번에 회전할 각도
    public float rotationSpeed = 50f;  // 회전 속도
    private bool isRotating = false;  // 회전 중인지 여부
    private float targetRotationY;  // 목표 Y축 회전각도
    private bool isMouseOver = false;  // 마우스가 오브젝트 위에 있는지 여부

    private void Update()
    {
        // 마우스가 오브젝트 위에 있을 때 X키를 눌렀을 때만 45도씩 회전
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
        // 마우스가 오브젝트 위로 들어왔을 때 호출
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        // 마우스가 오브젝트에서 나갔을 때 호출
        isMouseOver = false;
    }

    void StartRotation()
    {
        isRotating = true;  // 회전 시작
        targetRotationY = transform.eulerAngles.y + rotationAmount;  // 목표 회전각 설정
    }

    void ContinueRotation()
    {
        // 부드럽게 목표 각도로 회전
        float newYRotation = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotationY, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newYRotation, transform.eulerAngles.z);

        // 목표 각도에 도달하면 회전 중지
        if (Mathf.Abs(newYRotation - targetRotationY) < 0.01f)
        {
            isRotating = false;
        }
    }
}
