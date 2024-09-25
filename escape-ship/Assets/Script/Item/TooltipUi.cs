using TMPro;
using UnityEngine;

public class TooltipUI : Singleton<TooltipUI>
{
    [SerializeField] private CanvasGroup canvasGroup;  // 툴팁 패널의 CanvasGroup
    [SerializeField] private TextMeshProUGUI tooltipText;  // 아이템 설명을 표시하는 Text

    private void Start()
    {
        HideTooltip();  // 시작 시 툴팁 숨기기
        canvasGroup.blocksRaycasts = false;  // 툴팁이 Raycast를 받지 않게 설정
    }

    public void ShowTooltip(string description)
    {
        tooltipText.text = description;  // 툴팁에 설명 넣기
        canvasGroup.alpha = 1;  // 툴팁을 보이게 함
    }

    public void HideTooltip()
    {
        canvasGroup.alpha = 0;  // 툴팁을 숨김
    }

    public void SetTooltipPosition(Vector3 position)
    {
        transform.position = position;
    }
}
