using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FadeText : MonoBehaviour
{
    public TextMeshProUGUI myTMPText; // TMP Text UI 요소를 연결

    void OnEnable()
    {
        // 처음에 투명도를 0으로 설정하여 보이지 않게 함
        myTMPText.color = new Color(myTMPText.color.r, myTMPText.color.g, myTMPText.color.b, 0);

        // 페이드 인, 페이드 아웃을 반복하도록 설정
        StartFadeLoop();
    }

    // 페이드 인/아웃 반복 메서드
    private void StartFadeLoop()
    {
        // 3초 동안 투명도를 1로 변경하여 텍스트가 서서히 나타나도록 설정
        myTMPText.DOFade(1, 3f)
            .OnComplete(() =>
            {
                // 페이드 인 완료 후 페이드 아웃 시작
                myTMPText.DOFade(0, 3f).OnComplete(() =>
                {
                    // 페이드 아웃 완료 후 다시 반복
                    StartFadeLoop(); // 재귀적으로 페이드 인/아웃을 반복
                });
            });
    }
}
