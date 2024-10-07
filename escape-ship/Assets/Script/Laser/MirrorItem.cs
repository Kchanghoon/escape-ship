using UnityEngine;
using DG.Tweening;  // DoTween 네임스페이스 추가

public class MirrorItem : MonoBehaviour
{
    public GameObject mirror;  // 회전시킬 거울 오브젝트
    public GameObject mirror2;  // 회전시킬 두 번째 거울 오브젝트
    public float rotationAmount = 45f;  // 한 번에 회전할 각도
    public float rotationDuration = 0.5f;  // 회전 시간 (DoTween에서 사용할 애니메이션 시간)
    public KeyCode rotateKey = KeyCode.X;  // 회전시킬 때 누를 키
    private bool isMouseOver = false;  // 마우스가 버튼 위에 있는지 여부

    private void Update()
    {
        // 마우스가 버튼 위에 있을 때, 지정된 키를 눌렀을 때만 거울을 회전
        if (isMouseOver && Input.GetKeyDown(rotateKey))
        {
            StartRotation();
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
        if (mirror != null && mirror2 != null)
        {
            // 목표 회전 각도 설정
            float targetRotationY1 = mirror.transform.eulerAngles.y + rotationAmount;
            float targetRotationY2 = mirror2.transform.eulerAngles.y + rotationAmount;

            // DoTween을 사용하여 두 거울을 각각 부드럽게 회전
            mirror.transform.DORotate(new Vector3(0, targetRotationY1, 0), rotationDuration)
                .SetEase(Ease.OutQuad);  // 자연스럽게 감속

            mirror2.transform.DORotate(new Vector3(0, targetRotationY2, 0), rotationDuration)
                .SetEase(Ease.OutQuad);  // 자연스럽게 감속
        }
        else
        {
            Debug.LogWarning("Mirror 오브젝트가 할당되지 않았습니다!");
        }
    }
}
