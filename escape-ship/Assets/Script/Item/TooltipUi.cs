using TMPro;
using UnityEngine;

public class TooltipUI : Singleton<TooltipUI>
{
    [SerializeField] private CanvasGroup canvasGroup;  // ���� �г��� CanvasGroup
    [SerializeField] private TextMeshProUGUI tooltipText;  // ������ ������ ǥ���ϴ� Text

    private void Start()
    {
        HideTooltip();  // ���� �� ���� �����
        canvasGroup.blocksRaycasts = false;  // ������ Raycast�� ���� �ʰ� ����
    }

    public void ShowTooltip(string description)
    {
        tooltipText.text = description;  // ������ ���� �ֱ�
        canvasGroup.alpha = 1;  // ������ ���̰� ��
    }

    public void HideTooltip()
    {
        canvasGroup.alpha = 0;  // ������ ����
    }

    public void SetTooltipPosition(Vector3 position)
    {
        transform.position = position;
    }
}
