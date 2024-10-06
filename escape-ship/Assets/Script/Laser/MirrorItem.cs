using UnityEngine;

public class MirrorItem : MonoBehaviour
{
    public GameObject mirror;  // 회전시킬 거울 오브젝트
    public GameObject mirror2;  // 회전시킬 거울 오브젝트
    public float rotationAmount = 45f;  // 한 번에 회전할 각도
    public float rotationSpeed = 50f;  // 회전 속도
    public KeyCode rotateKey = KeyCode.X;  // 회전시킬 때 누를 키
    private bool isRotating = false;  // 회전 중인지 여부
    private float targetRotationY;  // 목표 Y축 회전각도
    private bool isMouseOver = false;  // 마우스가 버튼 위에 있는지 여부

    private void Update()
    {
        // 마우스가 버튼 위에 있을 때, 지정된 키를 눌렀을 때만 거울을 회전
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
        // 마우스가 버튼 오브젝트 위로 들어왔을 때 호출
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        // 마우스가 버튼 오브젝트에서 나갔을 때 호출
        isMouseOver = false;
    }

    void StartRotation()
    {
        if (mirror != null)
        {
            isRotating = true;  // 회전 시작
            targetRotationY = mirror.transform.eulerAngles.y + rotationAmount;  // 거울의 목표 회전각 설정
            targetRotationY = mirror2.transform.eulerAngles.y + rotationAmount;  // 거울의 목표 회전각 설정
        }
        else
        {
            Debug.LogWarning("Mirror 오브젝트가 할당되지 않았습니다!");
        }
    }

    void ContinueRotation()
    {
        // 부드럽게 목표 각도로 거울을 회전
        if (mirror != null)
        {
            float newYRotation = Mathf.MoveTowardsAngle(mirror.transform.eulerAngles.y, targetRotationY, rotationSpeed * Time.deltaTime);
            mirror.transform.eulerAngles = new Vector3(mirror.transform.eulerAngles.x, newYRotation, mirror.transform.eulerAngles.z);
            mirror2.transform.eulerAngles = new Vector3(mirror.transform.eulerAngles.x, newYRotation, mirror.transform.eulerAngles.z);

            // 목표 각도에 도달하면 회전 중지
            if (Mathf.Abs(newYRotation - targetRotationY) < 0.01f)
            {
                isRotating = false;
            }
        }
    }
}
