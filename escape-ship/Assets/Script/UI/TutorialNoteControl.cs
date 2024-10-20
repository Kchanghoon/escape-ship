using UnityEngine;
using UnityEngine.UI;  // UI 버튼과 패널을 조작하기 위해 필요

public class TutorialNoteControl : MonoBehaviour
{
    [SerializeField] private GameObject panel1;  // 첫 번째 패널
    [SerializeField] private GameObject panel2;  // 두 번째 패널
    [SerializeField] private Button nextButton;  // Next 버튼
    [SerializeField] private Button undoButton;  // Undo 버튼
    [SerializeField] private Button deleteButton;  // Delete 버튼
    [SerializeField] private GameObject tutorialPanel;  // 튜토리얼 패널

    private int currentPanelIndex = 1;  // 현재 활성화된 패널을 추적 (1 또는 2)

    void Start()
    {
        // 패널 초기 상태 설정 (시작할 때 panel1만 활성화)
        panel1.SetActive(true);
        panel2.SetActive(false);
        undoButton.gameObject.SetActive(false);

        // 버튼 클릭 이벤트 리스너 연결
        nextButton.onClick.AddListener(OnNextButtonClicked);
        undoButton.onClick.AddListener(OnUndoButtonClicked);
        deleteButton.onClick.AddListener(OnDeleteButtonClicked);
    }

    // Next 버튼을 클릭했을 때 호출되는 함수
    public void OnNextButtonClicked()
    {
        if (currentPanelIndex == 1)
        {
            // Panel1에서 Panel2로 넘어가기
            panel1.SetActive(false);
            panel2.SetActive(true);
            nextButton.gameObject.SetActive(false);
            undoButton.gameObject.SetActive(true);
            currentPanelIndex = 2;
        }
    }

    // Undo 버튼을 클릭했을 때 호출되는 함수
    public void OnUndoButtonClicked()
    {
        if (currentPanelIndex == 2)
        {
            // Panel2에서 Panel1로 돌아가기
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
