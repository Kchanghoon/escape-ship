using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FadeText : MonoBehaviour
{
    public TextMeshProUGUI myTMPText; // TMP Text UI 요소를 연결

    private void Start()
    {
        Debug.Log("[FadeText] Start() called. Initializing OnEnable().");
        OnEnable();
    }

    void OnEnable()
    {
        Debug.Log("[FadeText] OnEnable() called. Setting text alpha to 0.");

        // 처음에 투명도를 0으로 설정하여 보이지 않게 함
        if (myTMPText != null)
        {
            myTMPText.color = new Color(myTMPText.color.r, myTMPText.color.g, myTMPText.color.b, 0);
            Debug.Log("[FadeText] Text alpha successfully set to 0.");
        }
        else
        {
            Debug.LogError("[FadeText] myTMPText is null! Make sure it is assigned in the Inspector.");
        }

        // 페이드 인, 페이드 아웃을 반복하도록 설정
        StartFadeLoop();
    }

    // 페이드 인/아웃 반복 메서드
    private void StartFadeLoop()
    {
        Debug.Log("[FadeText] Starting fade-in animation.");

        // 3초 동안 투명도를 1로 변경하여 텍스트가 서서히 나타나도록 설정
        myTMPText.DOFade(1, 3f)
            .OnComplete(() =>
            {
                Debug.Log("[FadeText] Fade-in complete. Starting fade-out animation.");

                // 페이드 인 완료 후 페이드 아웃 시작
                myTMPText.DOFade(0, 3f).OnComplete(() =>
                {
                    Debug.Log("[FadeText] Fade-out complete. Restarting fade loop.");

                    // 페이드 아웃 완료 후 다시 반복
                    StartFadeLoop(); // 재귀적으로 페이드 인/아웃을 반복
                });
            });
    }
}
