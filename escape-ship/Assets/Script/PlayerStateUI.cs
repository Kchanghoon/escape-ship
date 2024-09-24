using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private Slider oxygenSlider;  // ��� ��ġ�� �����̴�
    [SerializeField] private Slider stressSlider;  // ��Ʈ���� ��ġ�� �����̴�
    [SerializeField] private Image oxygenFillImage;  // ��� ���� ������ ������ �̹���
    [SerializeField] private Image stressFillImage;  // ��Ʈ���� ���� ������ ������ �̹���
    [SerializeField] private Gradient oxygenGradient;  // ��� ���� ���� ��ȭ
    [SerializeField] private Gradient stressGradient;  // ��Ʈ���� ���� ���� ��ȭ

    [SerializeField] private TextMeshProUGUI oxygenText;  // ��� ��ġ �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI stressText;  // ��Ʈ���� ��ġ �ؽ�Ʈ
    
    private PlayerState playerState;  // �÷��̾� ���� ��ũ��Ʈ ����

    private void Start()
    {
        playerState = FindObjectOfType<PlayerState>();  // �÷��̾� ���� ��ũ��Ʈ ����

        if (playerState == null)
        {
            Debug.LogError("PlayerState ��ũ��Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        if (oxygenSlider == null || stressSlider == null || oxygenText == null || stressText == null)
        {
            Debug.LogError("�����̴� �Ǵ� �ؽ�Ʈ�� ����� ������� �ʾҽ��ϴ�.");
            return;
        }

        // �ʱ� �� ����
        UpdateOxygenUI(playerState.Oxygen);
        UpdateStressUI(playerState.Stress);
    }

    private void Update()
    {
        if (playerState == null) return;

        // �ǽð����� ��� �� ��Ʈ���� ��ġ�� UI�� �ݿ�
        UpdateOxygenUI(playerState.Oxygen);
        UpdateStressUI(playerState.Stress);
    }

    // ��� UI ������Ʈ
    private void UpdateOxygenUI(float currentOxygen)
    {
        if (oxygenSlider != null && oxygenFillImage != null && oxygenText != null)
        {
            oxygenSlider.value = currentOxygen / playerState.MaxOxygen;  // ��� ������ �����̴� ������ ����
            oxygenFillImage.color = oxygenGradient.Evaluate(oxygenSlider.value);  // ��� ��ġ�� ���� ���� ����
            oxygenText.text = $"{currentOxygen:0}/{playerState.MaxOxygen}";  // ��� ��ġ �ؽ�Ʈ ������Ʈ
        }
    }

    // ��Ʈ���� UI ������Ʈ
    private void UpdateStressUI(float currentStress)
    {
        if (stressSlider != null && stressFillImage != null && stressText != null)
        {
            stressSlider.value = currentStress / playerState.MaxStress;  // ��Ʈ���� ������ �����̴� ������ ����
            stressFillImage.color = stressGradient.Evaluate(stressSlider.value);  // ��Ʈ���� ��ġ�� ���� ���� ����
            stressText.text = $"{currentStress:0}/{playerState.MaxStress}";  // ��Ʈ���� ��ġ �ؽ�Ʈ ������Ʈ
        }
    }
}
