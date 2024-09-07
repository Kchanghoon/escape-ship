using UnityEngine;
using DG.Tweening;

public class ChestOpen : MonoBehaviour
{
    [SerializeField] private Transform lid;  // 상자의 뚜껑 (회전할 부분)
    [SerializeField] private Vector3 closedRotation;  // 닫혀있을 때의 회전값 (예: (0, 0, 0))
    [SerializeField] private Vector3 openRotation;    // 열렸을 때의 회전값 (예: (-90, 0, 0))
    [SerializeField] private float duration = 1f;  // 열리는 속도
    [SerializeField] private Ease motionEase = Ease.InOutQuad;  // 애니메이션 Ease
    private bool isOpen = false;  // 상자가 열렸는지 여부를 기록

    void Start()
    {
        lid.localRotation = Quaternion.Euler(closedRotation);  // 처음엔 닫힌 상태로 시작
    }

    void Update()
    {
        // E 키를 눌렀을 때 상자를 여닫는 기능
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleChest();  // 상자를 열거나 닫음
        }
    }

    // 상자를 열거나 닫는 함수
    private void ToggleChest()
    {
        if (isOpen)
        {
            // 상자가 열려있으면 닫기
            lid.DORotate(closedRotation, duration).SetEase(motionEase);
        }
        else
        {
            // 상자가 닫혀있으면 열기
            lid.DORotate(openRotation, duration).SetEase(motionEase);
        }

        isOpen = !isOpen;  // 상태를 반전시킴
    }
}
