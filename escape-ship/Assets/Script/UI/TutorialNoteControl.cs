using UnityEngine;
using UnityEngine.UI;  // UI ��ư�� �г��� �����ϱ� ���� �ʿ�

public class TutorialNoteControl : MonoBehaviour
{
    [SerializeField] private GameObject panel1;  // ù ��° �г�
    [SerializeField] private GameObject panel2;  // �� ��° �г�
    [SerializeField] private Button nextButton;  // Next ��ư
    [SerializeField] private Button undoButton;  // Undo ��ư
    [SerializeField] private Button deleteButton;  // Delete ��ư
    [SerializeField] private GameObject tutorialPanel;  // Ʃ�丮�� �г�

    private int currentPanelIndex = 1;  // ���� Ȱ��ȭ�� �г��� ���� (1 �Ǵ� 2)

    void Start()
    {
        // �г� �ʱ� ���� ���� (������ �� panel1�� Ȱ��ȭ)
        panel1.SetActive(true);
        panel2.SetActive(false);
        undoButton.gameObject.SetActive(false);

        // ��ư Ŭ�� �̺�Ʈ ������ ����
        nextButton.onClick.AddListener(OnNextButtonClicked);
        undoButton.onClick.AddListener(OnUndoButtonClicked);
        deleteButton.onClick.AddListener(OnDeleteButtonClicked);
    }

    // Next ��ư�� Ŭ������ �� ȣ��Ǵ� �Լ�
    public void OnNextButtonClicked()
    {
        if (currentPanelIndex == 1)
        {
            // Panel1���� Panel2�� �Ѿ��
            panel1.SetActive(false);
            panel2.SetActive(true);
            nextButton.gameObject.SetActive(false);
            undoButton.gameObject.SetActive(true);
            currentPanelIndex = 2;
        }
    }

    // Undo ��ư�� Ŭ������ �� ȣ��Ǵ� �Լ�
    public void OnUndoButtonClicked()
    {
        if (currentPanelIndex == 2)
        {
            // Panel2���� Panel1�� ���ư���
            panel2.SetActive(false);
            panel1.SetActive(true);
            nextButton.gameObject.SetActive(true);
            undoButton.gameObject.SetActive(false);
            currentPanelIndex = 1;
        }
    }

    public void OnDeleteButtonClicked()
    {
        tutorialPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
