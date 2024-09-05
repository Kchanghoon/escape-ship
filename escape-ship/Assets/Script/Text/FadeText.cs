using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FadeText : MonoBehaviour
{
    public TextMeshProUGUI myTMPText; // TMP Text UI ��Ҹ� ����

    void Start()
    {
        // ó���� ������ 0���� �����Ͽ� ������ �ʰ� ��
        myTMPText.color = new Color(myTMPText.color.r, myTMPText.color.g, myTMPText.color.b, 0);

        // 2�� ���� ������ 1�� �����Ͽ� �ؽ�Ʈ�� ������ ��Ÿ������ ����
        myTMPText.DOFade(1, 3f).SetLoops(-1, LoopType.Yoyo);  // ���� �ݺ�
    }
}