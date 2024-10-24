using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FadeText : MonoBehaviour
{
    public TextMeshProUGUI myTMPText; // TMP Text UI ��Ҹ� ����

    void OnEnable()
    {
        // ó���� ������ 0���� �����Ͽ� ������ �ʰ� ��
        myTMPText.color = new Color(myTMPText.color.r, myTMPText.color.g, myTMPText.color.b, 0);

        // ���̵� ��, ���̵� �ƿ��� �ݺ��ϵ��� ����
        StartFadeLoop();
    }

    // ���̵� ��/�ƿ� �ݺ� �޼���
    private void StartFadeLoop()
    {
        // 3�� ���� ������ 1�� �����Ͽ� �ؽ�Ʈ�� ������ ��Ÿ������ ����
        myTMPText.DOFade(1, 3f)
            .OnComplete(() =>
            {
                // ���̵� �� �Ϸ� �� ���̵� �ƿ� ����
                myTMPText.DOFade(0, 3f).OnComplete(() =>
                {
                    // ���̵� �ƿ� �Ϸ� �� �ٽ� �ݺ�
                    StartFadeLoop(); // ��������� ���̵� ��/�ƿ��� �ݺ�
                });
            });
    }
}
