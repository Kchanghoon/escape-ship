using UnityEngine;
using TMPro;

public class DraggableTrigger : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public GameObject puzzlePanel;  // 퍼즐 UI 패널
    public float interactDistance = 3f;  // 플레이어와 오브젝트 간의 상호작용 가능한 거리
    public TextMeshProUGUI interactText;  // 상호작용 안내 텍스트 (TextMeshPro 사용)
    public DragBlockManager blockManager;  // DragBlockManager 참조
    public RectTransform endPosition;  // EndPosition 참조
    public RectTransform targetBlock;  // TargetBlock 참조

    [Header("Dialogue Settings")]
    public GameObject dialogueUI;  // 대화 UI 패널
    public string[] dialogues;  // 대화 내용 배열
    public TextMeshProUGUI dialogueText;  // 대화 텍스트 표시용
    private int currentDialogueIndex = 0;  // 현재 대화 인덱스
    private bool isDialogueActive = false;  // 대화가 활성화되었는지 여부

    public bool isTimeStopped = true;  // 대화 중에 시간을 멈출지 여부
    private Transform playerTransform;  // 플레이어의 Transform을 저장
    private bool isMouseOverObject = false;  // 마우스가 오브젝트 위에 있는지 여부를 추적
    private bool isPuzzleCompleted = false;  // 퍼즐이 완료되었는지 여부를 추적

    private float positionTolerance = 5f;  // 목표 위치에 도달했는지 확인할 때의 허용 오차

    void Start()
    {
        // 게임 시작 시 플레이어의 Transform을 찾음
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // 상호작용 텍스트 비활성화
        interactText.gameObject.SetActive(false);

        // 대화 UI와 퍼즐 패널 비활성화
        dialogueUI.SetActive(false);
        puzzlePanel.SetActive(false);

        // 블록들을 그리드에 맞게 초기화
        blockManager.SnapAllBlocksToGrid();
    }

    // 마우스가 오브젝트 위에 있을 때 호출되는 함수
    private void OnMouseEnter()
    {
        if (!isPuzzleCompleted)
        {
            isMouseOverObject = true;
            HighlightObject(true);
        }
    }

    // 마우스가 오브젝트에서 벗어날 때 호출되는 함수
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
                    interactText.text = "E키를 눌러 대화를 시작하세요.";

                    // E 키를 눌렀을 때 대화 시작
                    if (Input.GetKeyDown(KeyCode.E) && !isDialogueActive)
                    {
                        StartDialogue();  // 대화 시작
                    }
                }
                else
                {
                    interactText.gameObject.SetActive(true);
                    interactText.text = "이미 해결된 퍼즐입니다.";
                }
            }
            else
            {
                interactText.gameObject.SetActive(false);
            }
        }

            // 대화 중일 때 F키를 눌러 다음 대화로 넘어감
            if (isDialogueActive && Input.GetKeyDown(KeyCode.F))
            {
                ShowNextDialogue();
            }
        
    }

    // 대화 시작
    void StartDialogue()
    {
        dialogueUI.SetActive(true);  // 대화 UI 활성화
        currentDialogueIndex = 0;  // 대화 시작 인덱스 초기화
        isDialogueActive = true;
        if (isTimeStopped)
        {
            Time.timeScale = 0f;  // 시간을 멈춤
        }
        ShowNextDialogue();  // 첫 대화 표시
    }

    // 다음 대화를 표시하는 함수
    void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];  // 대화 표시
            currentDialogueIndex++;  // 인덱스 증가
        }
        else
        {
            EndDialogue();  // 대화 종료
        }
    }

    // 대화 종료 시 호출되는 함수
    void EndDialogue()
    {
        dialogueUI.SetActive(false);  // 대화 UI 비활성화
        isDialogueActive = false;
        if (isTimeStopped)
        {
            Time.timeScale = 1f;  // 시간을 다시 흐르게 함
        }

        // 대화가 끝난 후 퍼즐 패널을 열기
        OpenPuzzlePanel();
    }

    // 퍼즐 패널을 여는 함수
    void OpenPuzzlePanel()
    {
        puzzlePanel.SetActive(true);
        Time.timeScale = 0f;  // 퍼즐 푸는 동안 시간 정지
        interactText.gameObject.SetActive(false);
    }

    // 퍼즐 완료 확인 함수 (위치 기반으로 목표에 도달했는지 확인)
    public void CheckPuzzleCompletion()
    {
        if (isPuzzleCompleted) return;

        if (IsTargetAtEndPosition())
        {
            CompletePuzzle();  // 퍼즐 완료
        }
    }

    private bool IsTargetAtEndPosition()
    {
        float tolerance = 50f;

        // TargetBlock과 EndPosition의 anchoredPosition 비교
        float distance = Mathf.Abs(targetBlock.anchoredPosition.x - endPosition.anchoredPosition.x);

        return distance <= tolerance;
    }




    // 퍼즐 완료 처리
    void CompletePuzzle()
    {
        isPuzzleCompleted = true;
        puzzlePanel.SetActive(false);  // 퍼즐 패널 비활성화

        var itemController = ItemController.Instance;
        itemController.AddItem("12");  // 아이템 추가

        Time.timeScale = 1f;  // 시간 다시 정상화
        Debug.Log("퍼즐 완료!");
    }

    // 오브젝트를 하이라이트 처리
    void HighlightObject(bool highlight)
    {
        var outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = highlight;
        }
    }
}
