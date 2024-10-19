using UnityEngine;
using TMPro;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject puzzlePanel;  // ���� �г� UI
    public float interactDistance = 3f;  // ��ȣ�ۿ� ������ �Ÿ�
    public TextMeshProUGUI interactText;  // ��ȣ�ۿ� �ؽ�Ʈ (TextMeshPro ��� ��)
    private Transform playerTransform;  // �÷��̾��� Transform
    private bool isMouseOverObject = false;  // ���콺�� ������Ʈ�� ��Ҵ��� ����
    private bool isPuzzleCompleted = false;  // ������ �Ϸ�Ǿ����� ����
    public DropSlot[] dropSlots;  // ������ ��� DropSlot

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // �÷��̾��� Transform�� ������
        KeyManager.Instance.keyDic[KeyAction.Play] += OpenPuzzlePanel;
    }

    // ���콺�� ������Ʈ�� ����� �� ȣ��
    private void OnMouseEnter()
    {
        isMouseOverObject = true;  // ���콺�� ������Ʈ�� ����
        HighlightObject(true);  // ������Ʈ ���̶���Ʈ Ȱ��ȭ (������)
    }

    // ���콺�� ������Ʈ���� ����� �� ȣ��
    private void OnMouseExit()
    {
        isMouseOverObject = false;  // ���콺�� ������Ʈ���� ���
        interactText.gameObject.SetActive(false);  // ���콺�� ����� ��ȣ�ۿ� �ؽ�Ʈ ��Ȱ��ȭ
        HighlightObject(false);  // ������Ʈ ���̶���Ʈ ��Ȱ��ȭ (������)
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
                    interactText.gameObject.SetActive(true);  // ��ȣ�ۿ� �ؽ�Ʈ Ȱ��ȭ
                    interactText.text = "EŰ�� ���� ���� �г��� ������";  // �ؽ�Ʈ ����

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        OpenPuzzlePanel();
                    }
                }
                else
                {
                    // �̹� ������ Ǯ���� ���
                    interactText.gameObject.SetActive(true);  // �ؽ�Ʈ Ȱ��ȭ
                    interactText.text = "�̹� �ذ��� �����Դϴ�.";  // �̹� �ذ�� ���� �޽���
                }
            }
            else
            {
                interactText.gameObject.SetActive(false);  // �÷��̾ �־����� �ؽ�Ʈ ��Ȱ��ȭ
            }
        }
    }

    // ���� �г��� ���� �Լ�
    void OpenPuzzlePanel()
    {
        if (!isPuzzleCompleted)  // ������ �̹� �Ϸ���� �ʾ��� ��쿡�� ����
        {
            puzzlePanel.SetActive(true);  // ���� �г� Ȱ��ȭ
            Time.timeScale = 0f;  // ���� �Ͻ�����
            interactText.gameObject.SetActive(false);  // ��ȣ�ۿ� �ؽ�Ʈ ��Ȱ��ȭ
        }
    }

    // ���� �Ϸ� �� ȣ���� �Լ�
    public void CompletePuzzle()
    {
        isPuzzleCompleted = true;  // ������ �Ϸ��
        puzzlePanel.SetActive(false);  // ���� �г� ��Ȱ��ȭ
        Time.timeScale = 1f;  // ���� �簳
        Debug.Log("���� �Ϸ�!");
    }

    // ��� DropSlot�� �ùٸ��� ��ġ�Ǿ����� Ȯ���ϴ� �Լ�
    public void CheckAllSlots()
    {
        foreach (DropSlot slot in dropSlots)
        {
            if (!slot.IsCorrectPiecePlaced())
            {
                return;  // �ϳ��� �ùٸ��� ��ġ���� �ʾҴٸ� �Լ� ����
            }
        }
        // ��� ������ �ùٸ��� ��ġ�� ��� ���� �Ϸ�
        CompletePuzzle();

        ItemController.Instance.AddItem("2");
    }

    // ������Ʈ ���̶���Ʈ �Լ� (���� ����)
    void HighlightObject(bool highlight)
    {
        var outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = highlight;  // ������Ʈ ���̶���Ʈ Ȱ��ȭ/��Ȱ��ȭ
        }
    }
}
