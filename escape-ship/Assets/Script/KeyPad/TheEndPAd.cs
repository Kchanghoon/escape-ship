using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TheEndPAd : MonoBehaviour
{

    [SerializeField] private Transform player;  // �÷��̾��� Transform
    [SerializeField] private float interactDistance = 3f;  // ��ȣ�ۿ� ������ �Ÿ�
    private bool isMouseOverItem = false;  // ���콺�� ������Ʈ ���� �ִ��� ���θ� ����
    [SerializeField] private TextMeshProUGUI statusText;  // ���� ���¸� ǥ���ϴ� TextMeshPro �ؽ�Ʈ
    [SerializeField] private float fadeDuration = 1f;  // �ؽ�Ʈ�� ������ ������� �ð�
    [SerializeField] CanvasGroup blackoutCanvasGroup;

    [Header("Dialogue Settings")]
    public GameObject dialogueUI;  // ��ȭ UI �г�
    public string[] dialogues;  // ��ȭ ���� �迭
    public TextMeshProUGUI dialogueText;  // ��ȭ �ؽ�Ʈ ǥ�ÿ�
    private int currentDialogueIndex = 0;  // ���� ��ȭ �ε���
    private bool isDialogueActive = false;  // ��ȭ�� Ȱ��ȭ�Ǿ����� ����

    public bool isTimeStopped = true;  // ��ȭ �߿� �ð��� ������ ����
    // Start is called before the first frame update
    void Start()
    {
        // ���� �ؽ�Ʈ ��Ȱ��ȭ
        statusText.gameObject.SetActive(false);

        KeyManager.Instance.keyDic[KeyAction.Play] += TryEnd;  // KeyManager���� Play Ű �̺�Ʈ ���
    }

    private void Update()
    {
        // ��ȭ ���� �� FŰ�� ���� ���� ��ȭ�� �Ѿ
        if (isDialogueActive && Input.GetKeyDown(KeyCode.F))
        {
            ShowNextDialogue();
        }
    }
    // ���콺�� ������Ʈ ���� ���� �� ȣ��Ǵ� �޼���
    private void OnMouseEnter()
    {
        isMouseOverItem = true;  // ���콺�� ������Ʈ ���� ����
    }

    // ���콺�� ������Ʈ���� ��� �� ȣ��Ǵ� �޼���
    private void OnMouseExit()
    {
        isMouseOverItem = false;  // ���콺�� ������Ʈ ���� ����
    }

    // ���� TryUpgrade �޼��� �״�� ����
    private void TryEnd()
    {
        // �÷��̾�� ������Ʈ ������ �Ÿ��� ���
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // ���õ� �������� ������, ���� �Ÿ� ���� �ְ�, ���콺�� ������ ���� �ִ��� Ȯ��
        if (distanceToPlayer <= interactDistance && isMouseOverItem)
        {
            // ItemController���� ���� ���� ���� ������ Ȯ��
            var itemController = ItemController.Instance;

            // �κ��丮���� 11���� 5�� �������� �ִ��� Ȯ��
            var item13 = itemController.curItemDatas.Find(x => x.id == "13");

            if (item13 != null)
            {
                Debug.Log("���� ��");
                StartCoroutine(EndScene());

            }
            else
            {
                StartDialogue();
            }
        }
    }
    void StartDialogue()
    {
        dialogueUI.SetActive(true);  // ��ȭ UI Ȱ��ȭ
        currentDialogueIndex = 0;  // ��ȭ ���� �ε��� �ʱ�ȭ
        isDialogueActive = true;
        if (isTimeStopped)
        {
            Time.timeScale = 0f;  // �ð��� ����
        }
        ShowNextDialogue();  // ù ��ȭ ǥ��
    }

    // ���� ��ȭ�� ǥ���ϴ� �Լ�
    void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];  // ��ȭ ǥ��
            currentDialogueIndex++;  // �ε��� ����
        }
        else
        {
            EndDialogue();  // ��ȭ ����
        }
    }

    // ��ȭ ���� �� ȣ��Ǵ� �Լ�
    void EndDialogue()
    {
        dialogueUI.SetActive(false);  // ��ȭ UI ��Ȱ��ȭ
        isDialogueActive = false;
        if (isTimeStopped)
        {
            Time.timeScale = 1f;  // �ð��� �ٽ� �帣�� ��
        }

        // ��ȭ�� ���� �� ���� �г��� ����
        ClosePanel();
    }

    void ClosePanel()
    {
        dialogueUI.SetActive(false);  // ���� �г� ��Ȱ��ȭ
    }

    private IEnumerator EndScene()
    {
        if (blackoutCanvasGroup != null)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                blackoutCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            blackoutCanvasGroup.alpha = 1;
        }
    }
}
