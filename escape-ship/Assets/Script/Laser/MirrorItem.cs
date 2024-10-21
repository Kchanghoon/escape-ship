using UnityEngine;
using DG.Tweening;  // DoTween 네임스페이스 추가

public class MirrorItem : MonoBehaviour
{
    public GameObject mirror;  // 첫 번째 회전시킬 거울 오브젝트
    public GameObject mirror2;  // 두 번째 회전시킬 거울 오브젝트
    public float rotationAmount = 45f;  // 한 번에 회전할 각도
    public float rotationDuration = 0.5f;  // 회전 시간 (DoTween에서 사용할 애니메이션 시간)
    private bool isMouseOver = false;  // 마우스가 버튼 위에 있는지 여부
    private bool isAnimating = false;  // 애니메이션이 진행 중인지 여부

    public void Start()
    {
        // 키 매니저에서 특정 키 액션에 퍼즐 패널을 여는 기능을 할당
        KeyManager.Instance.keyDic[KeyAction.PickUp] += StartRotation;
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
        // 애니메이션이 진행 중이 아니고, 마우스가 오브젝트 위에 있을 때만 실행
        if (isMouseOver && !isAnimating)
        {
            if (mirror != null && mirror2 != null )
            {
                // 애니메이션이 시작되었음을 표시
                isAnimating = true;

                // 목표 회전 각도 설정
                float targetRotationY1 = mirror.transform.localEulerAngles.y + rotationAmount;
                float targetRotationY2 = mirror2.transform.localEulerAngles.y + rotationAmount;

                // DoTween을 사용하여 세 거울을 각각 부드럽게 회전
                mirror.transform.DORotate(new Vector3(0, targetRotationY1, 0), rotationDuration)
                    .SetEase(Ease.OutQuad);  // 자연스럽게 감속

                mirror2.transform.DORotate(new Vector3(0, targetRotationY2, 0), rotationDuration)
                    .SetEase(Ease.OutQuad).OnComplete(() => isAnimating = false);  // 마지막 거울 애니메이션이 완료된 후 애니메이션 상태 해제;  // 자연스럽게 감속

            }
            else
            {
                Debug.LogWarning("Mirror 오브젝트가 할당되지 않았습니다!");
            }
        }
    }
}
