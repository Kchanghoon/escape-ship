using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;  // NPC�� ����� ��ȭ �г�
    public TextMeshProUGUI dialogueText;  // ��ȭ ���� ǥ���� �ؽ�Ʈ
    public string[] dialogues;  // NPC ��ȭ �����
    public float interactionDistance = 3f;  // NPC�� ��ȣ�ۿ��� �� �ִ� �Ÿ�
    private Transform playerTransform;  // �÷��̾��� Transform
    private bool isMouseOverNPC = false;  // ���콺�� NPC ���� �ִ��� ����
    private bool isDialogueActive = false;  // ��ȭ �г� ����
    private int currentDialogueIndex = 0;  // ���� ��ȭ �ε���
    private Canvas dialogueCanvas;  // ��ȭ �г��� Canvas
    public TextMeshProUGUI TalktoText;  // ȭ�鿡 ����� �ؽ�Ʈ (TextMeshPro ��� ��)

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // �÷��̾��� Transform ��������
        dialoguePanel.SetActive(false);  // ��ȭ �г� ó���� ��Ȱ��ȭ
                                         // ��ȭ �г��� Canvas ��������
        dialogueCanvas = dialoguePanel.GetComponentInParent<Canvas>();
        TalktoText.gameObject.SetActive(false);  // ó���� �ؽ�Ʈ�� ��Ȱ��ȭ
        // Ű �Է� �̺�Ʈ�� KeyManager�� keyDic�� ��� (PlayAction�� ����)
        KeyManager.Instance.keyDic[KeyAction.Play] += TryToggleDialoguePanel;

        // KeyAction.NextDialogue�� ��ȭ�� �ѱ� �� �ְ� ����
        KeyManager.Instance.keyDic[KeyAction.PickUp] += ShowNextDialogue;
    }

    // ���콺�� NPC ���� ���� �� ȣ��
    private void OnMouseEnter()
    {
        isMouseOverNPC = true;  // ���콺�� NPC ���� ����
        TalktoText.gameObject.SetActive(true);  // �ؽ�Ʈ Ȱ��ȭ
        TalktoText.text = "EŰ�� ���� ��ȭ�ϼ���";  // ���� ����
    }

    // ���콺�� NPC���� ����� �� ȣ��
    private void OnMouseExit()
    {
        isMouseOverNPC = false;  // ���콺�� NPC���� ���
        TalktoText.gameObject.SetActive(false);  // ������ ����� �ؽ�Ʈ ��Ȱ��ȭ
    }

    // Ű �Է����� ��ȭ �г��� ���� �Լ�
    private void TryToggleDialoguePanel()
    {
        // ���콺�� NPC ���� �ְ�, �÷��̾ ����� ��쿡�� ����
        if (isMouseOverNPC)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);

            if (distance <= interactionDistance)
            {
                ToggleDialoguePanel();
            }
            else
            {
                TalktoText.gameObject.SetActive(false);  // �Ÿ��� �־����� �ؽ�Ʈ ��Ȱ��ȭ
            }
        }
    }

    // �г��� ���ų� �ݴ� �Լ�
    private void ToggleDialoguePanel()
    {
        isDialogueActive = !isDialogueActive;  // �г� ���� ���
        dialoguePanel.SetActive(isDialogueActive);  // �г� Ȱ��ȭ/��Ȱ��ȭ

        if (isDialogueActive)
        {
            currentDialogueIndex = 0;  // ��ȭ�� ó������ ����
            ShowNextDialogue();

            // �ð��� ����
            Time.timeScale = 0f;
            // Canvas�� Sorting Order�� 100���� ���� (���� ���ϴ� ��ŭ ���� ����)
            dialogueCanvas.sortingOrder = 100;
        }
        else
        {
            // ��ȭ�� ������ �ð��� �ٽ� �������� ����
            Time.timeScale = 1f;
            dialogueCanvas.sortingOrder = 0;
        }
    }

    // ��ȭ ���� ǥ��
    private void ShowNextDialogue()
    {
        if (isDialogueActive)  // ��ȭ�� Ȱ��ȭ�Ǿ� ���� ���� ��ȭ ����
        {
            if (currentDialogueIndex < dialogues.Length)
            {
                dialogueText.text = dialogues[currentDialogueIndex];  // ��ȭ ���� ����
                currentDialogueIndex++;
            }
            else
            {
                ToggleDialoguePanel();  // ��ȭ�� ������ �г� �ݱ�
            }
        }
    }
}
