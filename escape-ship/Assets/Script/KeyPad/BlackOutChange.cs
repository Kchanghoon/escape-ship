using UnityEngine;
using System.Collections;

public class BlackOutChange : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackoutCanvasGroup;  // 블랙아웃 효과를 줄 CanvasGroup
    [SerializeField] private float blackoutDuration = 2f;  // 블랙아웃 지속 시간

    private void Start()
    {
        // 블랙아웃 시작 시 알파값을 0으로 설정해 화면이 투명하게 보이도록 설정
        if (blackoutCanvasGroup != null)
        {
            blackoutCanvasGroup.alpha = 0f;
        }
    }

    // 블랙아웃 효과 시작
    public IEnumerator StartBlackOut()
    {
        // 화면을 어둡게 함
        yield return StartCoroutine(FadeCanvasGroup(blackoutCanvasGroup, 0f, 1f, blackoutDuration));
    }

    // 블랙아웃 효과 제거
    public IEnumerator EndBlackOut()
    {
        // 화면을 밝게 함
        yield return StartCoroutine(FadeCanvasGroup(blackoutCanvasGroup, 1f, 0f, blackoutDuration));
    }

    // CanvasGroup의 Alpha 값을 서서히 변경하는 메서드
    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}
