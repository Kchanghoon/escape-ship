using UnityEngine;
using TMPro;  // TextMeshPro를 사용한다면 필요

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject puzzlePanel; // 퍼즐 패널 UI
    public float interactDistance = 3f; // 상호작용 가능한 거리
    public TextMeshProUGUI interactText; // 상호작용 텍스트 (TextMeshPro 사용 시)
    private Transform playerTransform; // 플레이어의 Transform
    private bool isMouseOverObject = false; // 마우스가 오브젝트에 닿았는지 여부
    private bool isPuzzleCompleted = false; // 퍼즐이 완료되었는지 여부

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // 플레이어의 Transform을 가져옴
        KeyManager.Instance.keyDic[KeyAction.Play] += OpenPuzzlePanel;
    }

    // 마우스가 오브젝트에 닿았을 때 호출
    private void OnMouseEnter()
    {
        isMouseOverObject = true;  // 마우스가 오브젝트에 닿음
        HighlightObject(true); // 오브젝트 하이라이트 활성화 (선택적)
    }

    // 마우스가 오브젝트에서 벗어났을 때 호출
    private void OnMouseExit()
    {
        isMouseOverObject = false;  // 마우스가 오브젝트에서 벗어남
        interactText.gameObject.SetActive(false);  // 마우스가 벗어나면 상호작용 텍스트 비활성화
        HighlightObject(false); // 오브젝트 하이라이트 비활성화 (선택적)
    }

    void Update()
    {
        if (isMouseOverObject)
        {
            // 플레이어와 오브젝트 사이의 거리 계산
            float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

            // 플레이어가 지정된 거리 안에 있을 경우
            if (distanceToPlayer <= interactDistance)
            {
                if (!isPuzzleCompleted)
                {
                    interactText.gameObject.SetActive(true);  // 상호작용 텍스트 활성화
                    interactText.text = "E키를 눌러 퍼즐 패널을 여세요";  // 텍스트 설정

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        OpenPuzzlePanel();
                    }
                }
                else
                {
                    // 이미 퍼즐을 풀었을 경우
                    interactText.gameObject.SetActive(true);  // 텍스트 활성화
                    interactText.text = "이미 해결한 퀴즈입니다.";  // 이미 해결된 퍼즐 메시지
                }
            }
            else
            {
                interactText.gameObject.SetActive(false);  // 플레이어가 멀어지면 텍스트 비활성화
            }
        }
    }

    // 퍼즐 패널을 여는 함수
    void OpenPuzzlePanel()
    {
        if (!isPuzzleCompleted)  // 퍼즐이 이미 완료되지 않았을 경우에만 실행
        {
            puzzlePanel.SetActive(true);  // 퍼즐 패널 활성화
            Time.timeScale = 0f;  // 게임 일시정지
            interactText.gameObject.SetActive(false);  // 상호작용 텍스트 비활성화
        }
    }

    // 퍼즐 완료 시 호출할 함수
    public void CompletePuzzle()
    {
        isPuzzleCompleted = true;  // 퍼즐이 완료됨
        puzzlePanel.SetActive(false);  // 퍼즐 패널 비활성화
        Time.timeScale = 1f;  // 게임 재개
        Debug.Log("퍼즐 완료!");
    }

    // 오브젝트 하이라이트 함수 (선택 사항)
    void HighlightObject(bool highlight)
    {
        var outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = highlight;  // 오브젝트 하이라이트 활성화/비활성화
        }
    }
}
