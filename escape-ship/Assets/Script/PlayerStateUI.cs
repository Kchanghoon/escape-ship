using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private Slider oxygenSlider;  // 산소 수치용 슬라이더
    [SerializeField] private Slider stressSlider;  // 스트레스 수치용 슬라이더
    [SerializeField] private Image oxygenFillImage;  // 산소 바의 색상을 변경할 이미지
    [SerializeField] private Image stressFillImage;  // 스트레스 바의 색상을 변경할 이미지
    [SerializeField] private Gradient oxygenGradient;  // 산소 바의 색상 변화
    [SerializeField] private Gradient stressGradient;  // 스트레스 바의 색상 변화

    [SerializeField] private TextMeshProUGUI oxygenText;  // 산소 수치 텍스트
    [SerializeField] private TextMeshProUGUI stressText;  // 스트레스 수치 텍스트
    
    private PlayerState playerState;  // 플레이어 상태 스크립트 참조

    private void Start()
    {
        playerState = FindObjectOfType<PlayerState>();  // 플레이어 상태 스크립트 참조

        if (playerState == null)
        {
            Debug.LogError("PlayerState 스크립트를 찾을 수 없습니다.");
            return;
        }

        if (oxygenSlider == null || stressSlider == null || oxygenText == null || stressText == null)
        {
            Debug.LogError("슬라이더 또는 텍스트가 제대로 연결되지 않았습니다.");
            return;
        }

        // 초기 바 세팅
        UpdateOxygenUI(playerState.Oxygen);
        UpdateStressUI(playerState.Stress);
    }

    private void Update()
    {
        if (playerState == null) return;

        // 실시간으로 산소 및 스트레스 수치를 UI에 반영
        UpdateOxygenUI(playerState.Oxygen);
        UpdateStressUI(playerState.Stress);
    }

    // 산소 UI 업데이트
    private void UpdateOxygenUI(float currentOxygen)
    {
        if (oxygenSlider != null && oxygenFillImage != null && oxygenText != null)
        {
            oxygenSlider.value = currentOxygen / playerState.MaxOxygen;  // 산소 비율을 슬라이더 값으로 설정
            oxygenFillImage.color = oxygenGradient.Evaluate(oxygenSlider.value);  // 산소 수치에 따라 색상 변경
            oxygenText.text = $"{currentOxygen:0}/{playerState.MaxOxygen}";  // 산소 수치 텍스트 업데이트
        }
    }

    // 스트레스 UI 업데이트
    private void UpdateStressUI(float currentStress)
    {
        if (stressSlider != null && stressFillImage != null && stressText != null)
        {
            stressSlider.value = currentStress / playerState.MaxStress;  // 스트레스 비율을 슬라이더 값으로 설정
            stressFillImage.color = stressGradient.Evaluate(stressSlider.value);  // 스트레스 수치에 따라 색상 변경
            stressText.text = $"{currentStress:0}/{playerState.MaxStress}";  // 스트레스 수치 텍스트 업데이트
        }
    }
}
