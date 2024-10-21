using UnityEngine;
using TMPro;  // TextMeshPro ���̺귯�� �߰�

public class DialogueTriggerUI : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public GameObject dialogueUI;  // ǥ���� ��ȭ UI (�г�)
    public string[] dialogues;  // ��ȭ ���� �迭
    public bool isTimeStopped = true;  // ��ȭ �߿� �ð��� ������ ����
    private bool isDialogueActive = false;
    private int currentDialogueIndex = 0;  // ���� ��ȭ �ε���
    private bool hasTriggered = false;  // ��ȭ�� �̹� �ѹ� �߻��ߴ��� ����

    [Header("Player Settings")]
    public string playerTag = "Player";  // �÷��̾� �±�, �⺻���� "Player"
    private Transform playerTransform;  // �÷��̾� Transform�� ����
    public float interactionDistance = 3f;  // �÷��̾�� ��ȣ�ۿ��� �� �ִ� �Ÿ�

    [Header("Dialogue Text Reference")]
    public TextMeshProUGUI dialogueText;  // ��ȭ ������ ǥ���� TextMeshProUGUI

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;  // �÷��̾��� Transform ��������
        dialogueUI.SetActive(false);  // ������ �� UI�� ��Ȱ��ȭ
    }

    private void Update()
    {
        // ��ȭ ���� �� FŰ�� ���� ���� ��ȭ�� �Ѿ
        if (isDialogueActive && Input.GetKeyDown(KeyCode.F))
        {
            ShowNextDialogue();
        }
    }

    // �÷��̾ Ư�� ������ �� �� Ʈ���� �߻�
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !isDialogueActive && !hasTriggered)  // ��ȭ�� �� ���� �߻����� �ʾ��� ���� ����
        {
            float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

            if (distanceToPlayer <= interactionDistance)
            {
                ShowDialogue();  // ��ȭâ ǥ��
                hasTriggered = true;  // ��ȭ�� �߻������� ���
            }
        }
    }

    // ��ȭâ�� ǥ���ϴ� �޼���
    void ShowDialogue()
    {
        dialogueUI.SetActive(true);  // UI�� Ȱ��ȭ
        currentDialogueIndex = 0;  // ��ȭ ���� �� �ε����� 0���� ����
        ShowNextDialogue();  // ù ��ȭ ǥ��

        if (isTimeStopped)
        {
            Time.timeScale = 0f;  // �ð��� ����
        }

        isDialogueActive = true;
    }

    // ��ȭâ���� ��ȭ ������ ǥ���ϰ�, ������ ��ȭ�� â�� ����
    void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];  // ���� ��ȭ ������ UI�� ǥ��
            currentDialogueIndex++;  // ���� ��ȭ�� �غ�
        }
        else
        {
            CloseDialogue();  // ��ȭ�� ������ �� â �ݱ�
        }
    }

    // ��ȭ�� ����Ǹ� UI�� ����� �ð��� �簳�ϴ� �޼���
    public void CloseDialogue()
    {
        dialogueUI.SetActive(false);  // UI�� ��Ȱ��ȭ

        if (isTimeStopped)
        {
            Time.timeScale = 1f;  // �ð��� �ٽ� �帣�� ��
        }

        isDialogueActive = false;
    }
}
