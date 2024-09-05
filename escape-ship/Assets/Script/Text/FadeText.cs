using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FadeText : MonoBehaviour
{
    public TextMeshProUGUI myTMPText; // TMP Text UI 요소를 연결

    void Start()
    {
        // 처음에 투명도를 0으로 설정하여 보이지 않게 함
        myTMPText.color = new Color(myTMPText.color.r, myTMPText.color.g, myTMPText.color.b, 0);

        // 2초 동안 투명도를 1로 변경하여 텍스트가 서서히 나타나도록 설정
        myTMPText.DOFade(1, 3f).SetLoops(-1, LoopType.Yoyo);  // 무한 반복
    }
}