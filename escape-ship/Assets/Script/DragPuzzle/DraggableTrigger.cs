using UnityEngine;
using TMPro;

public class DraggableTrigger : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public GameObject puzzlePanel;  // ���� UI �г�
    public float interactDistance = 3f;  // �÷��̾�� ������Ʈ ���� ��ȣ�ۿ� ������ �Ÿ�
    public TextMeshProUGUI interactText;  // ��ȣ�ۿ� �ȳ� �ؽ�Ʈ (TextMeshPro ���)
    public DragBlockManager blockManager;  // DragBlockManager ����
    public RectTransform endPosition;  // EndPosition ����
    public RectTransform targetBlock;  // TargetBlock ����

    [Header("Dialogue Settings")]
    public GameObject dialogueUI;  // ��ȭ UI �г�
    public string[] dialogues;  // ��ȭ ���� �迭
    public TextMeshProUGUI dialogueText;  // ��ȭ �ؽ�Ʈ ǥ�ÿ�
    private int currentDialogueIndex = 0;  // ���� ��ȭ �ε���
    private bool isDialogueActive = false;  // ��ȭ�� Ȱ��ȭ�Ǿ����� ����

    public bool isTimeStopped = true;  // ��ȭ �߿� �ð��� ������ ����
    private Transform playerTransform;  // �÷��̾��� Transform�� ����
    private bool isMouseOverObject = false;  // ���콺�� ������Ʈ ���� �ִ��� ���θ� ����
    private bool isPuzzleCompleted = false;  // ������ �Ϸ�Ǿ����� ���θ� ����

    private float positionTolerance = 5f;  // ��ǥ ��ġ�� �����ߴ��� Ȯ���� ���� ��� ����

    void Start()
    {
        // ���� ���� �� �÷��̾��� Transform�� ã��
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // ��ȣ�ۿ� �ؽ�Ʈ ��Ȱ��ȭ
        interactText.gameObject.SetActive(false);

        // ��ȭ UI�� ���� �г� ��Ȱ��ȭ
        dialogueUI.SetActive(false);
        puzzlePanel.SetActive(false);

        // ��ϵ��� �׸��忡 �°� �ʱ�ȭ
        blockManager.SnapAllBlocksToGrid();
    }

    // ���콺�� ������Ʈ ���� ���� �� ȣ��Ǵ� �Լ�
    private void OnMouseEnter()
    {
        if (!isPuzzleCompleted)
        {
            isMouseOverObject = true;
            HighlightObject(true);
        }
    }

    // ���콺�� ������Ʈ���� ��� �� ȣ��Ǵ� �Լ�
    private void OnMouseExit()
    {
        isMouseOverObject = false;
        interactText.gameObject.SetActive(false);
        HighlightObject(false);
    }

    void Update()
    {
        if (isMouseOverObject)
        {
            float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
            if (distanceToPlayer <= interactDistance)
            {
                if (!isPuzzleCompleted)
                {
                    interactText.gameObject.SetActive(true);
                    interactText.text = "EŰ�� ���� ��ȭ�� �����ϼ���.";

                    // E Ű�� ������ �� ��ȭ ����
                    if (Input.GetKeyDown(KeyCode.E) && !isDialogueActive)
                    {
                        StartDialogue();  // ��ȭ ����
                    }
                }
                else
                {
                    interactText.gameObject.SetActive(true);
                    interactText.text = "�̹� �ذ�� �����Դϴ�.";
                }
            }
            else
            {
                interactText.gameObject.SetActive(false);
            }
        }

            // ��ȭ ���� �� FŰ�� ���� ���� ��ȭ�� �Ѿ
            if (isDialogueActive && Input.GetKeyDown(KeyCode.F))
            {
                ShowNextDialogue();
            }
        
    }

    // ��ȭ ����
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
        OpenPuzzlePanel();
    }

    // ���� �г��� ���� �Լ�
    void OpenPuzzlePanel()
    {
        puzzlePanel.SetActive(true);
        Time.timeScale = 0f;  // ���� Ǫ�� ���� �ð� ����
        interactText.gameObject.SetActive(false);
    }

    // ���� �Ϸ� Ȯ�� �Լ� (��ġ ������� ��ǥ�� �����ߴ��� Ȯ��)
    public void CheckPuzzleCompletion()
    {
        if (isPuzzleCompleted) return;

        if (IsTargetAtEndPosition())
        {
            CompletePuzzle();  // ���� �Ϸ�
        }
    }

    private bool IsTargetAtEndPosition()
    {
        float tolerance = 50f;

        // TargetBlock�� EndPosition�� anchoredPosition ��
        float distance = Mathf.Abs(targetBlock.anchoredPosition.x - endPosition.anchoredPosition.x);

        return distance <= tolerance;
    }




    // ���� �Ϸ� ó��
    void CompletePuzzle()
    {
        isPuzzleCompleted = true;
        puzzlePanel.SetActive(false);  // ���� �г� ��Ȱ��ȭ

        var itemController = ItemController.Instance;
        itemController.AddItem("12");  // ������ �߰�

        Time.timeScale = 1f;  // �ð� �ٽ� ����ȭ
        Debug.Log("���� �Ϸ�!");
    }

    // ������Ʈ�� ���̶���Ʈ ó��
    void HighlightObject(bool highlight)
    {
        var outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = highlight;
        }
    }
}
