using UnityEngine;
using System.Collections;

public class BlackOutChange : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackoutCanvasGroup;  // ���ƿ� ȿ���� �� CanvasGroup
    [SerializeField] private float blackoutDuration = 2f;  // ���ƿ� ���� �ð�

    private void Start()
    {
        // ���ƿ� ���� �� ���İ��� 0���� ������ ȭ���� �����ϰ� ���̵��� ����
        if (blackoutCanvasGroup != null)
        {
            blackoutCanvasGroup.alpha = 0f;
        }
    }

    // ���ƿ� ȿ�� ����
    public IEnumerator StartBlackOut()
    {
        // ȭ���� ��Ӱ� ��
        yield return StartCoroutine(FadeCanvasGroup(blackoutCanvasGroup, 0f, 1f, blackoutDuration));
    }

    // ���ƿ� ȿ�� ����
    public IEnumerator EndBlackOut()
    {
        // ȭ���� ��� ��
        yield return StartCoroutine(FadeCanvasGroup(blackoutCanvasGroup, 1f, 0f, blackoutDuration));
    }

    // CanvasGroup�� Alpha ���� ������ �����ϴ� �޼���
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
