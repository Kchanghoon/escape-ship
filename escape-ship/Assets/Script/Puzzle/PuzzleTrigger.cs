using UnityEngine;
using TMPro;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject puzzlePanel;  // 퍼즐 UI 패널
    public float interactDistance = 3f;  // 플레이어와 오브젝트 간의 상호작용 가능한 거리
    public TextMeshProUGUI interactText;  // 상호작용 안내 텍스트 (TextMeshPro 사용)
    private Transform playerTransform;  // 플레이어의 Transform을 저장
    private bool isMouseOverObject = false;  // 마우스가 오브젝트 위에 있는지 여부를 추적
    private bool isPuzzleCompleted = false;  // 퍼즐이 완료되었는지 여부를 추적
    private bool hasPuzzleBeenSolved = false; // 퍼즐이 이미 완료된 경우를 추적
    public DropSlot[] dropSlots;  // 퍼즐에 사용되는 드롭 슬롯 배열

    void Start()
    {
        // 게임 시작 시 플레이어의 Transform을 찾음
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;


        // 키 매니저에서 특정 키 액션에 퍼즐 패널을 여는 기능을 할당
        KeyManager.Instance.keyDic[KeyAction.Play] += OpenPuzzlePanel;
    }

    // 마우스가 오브젝트 위에 있을 때 호출되는 함수
    private void OnMouseEnter()
    {
        // 퍼즐이 완료되지 않은 경우에만 마우스 오버 처리
        if (!isPuzzleCompleted && !hasPuzzleBeenSolved)
        {
            isMouseOverObject = true;  // 마우스가 오브젝트 위에 있음을 표시
            HighlightObject(true);  // 오브젝트를 하이라이트(강조) 처리
        }
    }

    // 마우스가 오브젝트에서 벗어날 때 호출되는 함수
    private void OnMouseExit()
    {
        isMouseOverObject = false;  // 마우스가 오브젝트에서 벗어났음을 표시
        interactText.gameObject.SetActive(false);  // 상호작용 안내 텍스트 비활성화
        HighlightObject(false);  // 오브젝트 하이라이트 비활성화
    }

    // 매 프레임마다 호출되는 함수
    void Update()
    {
        if (isMouseOverObject)  // 마우스가 오브젝트 위에 있을 때
        {
            // 퍼즐이 이미 완료되었으면 상호작용 차단
            if (hasPuzzleBeenSolved)
            {
                interactText.gameObject.SetActive(true);
                interactText.text = "이미 해결된 퍼즐입니다.";
                return;  // 퍼즐이 완료되었으면 상호작용을 중단
            }

            // 플레이어와 오브젝트 간의 거리를 계산
            float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

            // 플레이어가 상호작용 가능한 거리 이내에 있는지 확인
            if (distanceToPlayer <= interactDistance)
            {
                if (!isPuzzleCompleted)  // 퍼즐이 아직 완료되지 않았다면
                {
                    interactText.gameObject.SetActive(true);  // 상호작용 안내 텍스트 활성화
                    interactText.text = "E키를 눌러 퍼즐을 여세요.";  // 상호작용 안내 문구 설정
                }
                else
                {
                    // 이미 퍼즐이 완료된 상태일 때
                    interactText.gameObject.SetActive(true);  // 상호작용 안내 텍스트 활성화
                    interactText.text = "이미 해결된 퍼즐입니다.";  // 이미 완료된 퍼즐임을 안내
                }
            }
            else
            {
                interactText.gameObject.SetActive(false);  // 플레이어가 거리를 벗어나면 안내 텍스트 비활성화
            }
        }
    }

    // 퍼즐 패널을 여는 함수
    void OpenPuzzlePanel()
    {
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
        if (!isPuzzleCompleted && distanceToPlayer <= interactDistance)  // 퍼즐이 아직 완료되지 않았을 때만 실행
        {
            puzzlePanel.SetActive(true);  // 퍼즐 UI 패널 활성화
            Time.timeScale = 0f;  // 게임 시간 멈춤 (퍼즐 푸는 동안 일시 정지)
            interactText.gameObject.SetActive(false);  // 상호작용 텍스트 비활성화
        }
    }

    // 퍼즐이 완료되었을 때 호출되는 함수
    public void CompletePuzzle()
    {
        isPuzzleCompleted = true;  // 퍼즐 완료 상태로 설정
        hasPuzzleBeenSolved = true;  // 퍼즐이 이미 완료되었음을 표시
        puzzlePanel.SetActive(false);  // 퍼즐 패널 비활성화

        var itemController = ItemController.Instance;
        itemController.AddItem("12");  // 아이템 추가

        Time.timeScale = 1f;  // 게임 시간 재개
        Debug.Log("퍼즐 완료!");  // 디버그 메시지 출력
    }

    // 모든 드롭 슬롯이 올바르게 배치되었는지 확인하는 함수
    public void CheckAllSlots()
    {
        foreach (DropSlot slot in dropSlots)  // 각 드롭 슬롯을 순회하며 확인
        {
            if (!slot.IsCorrectPiecePlaced())  // 슬롯에 올바른 퍼즐 조각이 놓이지 않았다면
            {
                return;  // 함수 종료 (퍼즐 미완료)
            }
        }
        // 모든 드롭 슬롯이 올바르게 배치되었으면 퍼즐 완료 처리
        CompletePuzzle();
    }

    // 오브젝트를 하이라이트(강조) 처리하는 함수
    void HighlightObject(bool highlight)
    {
        var outline = GetComponent<Outline>();  // 오브젝트의 Outline 컴포넌트를 가져옴
        if (outline != null)
        {
            outline.enabled = highlight;  // 하이라이트 활성화 또는 비활성화
        }
    }
}
