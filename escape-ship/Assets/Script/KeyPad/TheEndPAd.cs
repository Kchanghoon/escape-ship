using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TheEndPAd : MonoBehaviour
{

    [SerializeField] private Transform player;  // 플레이어의 Transform
    [SerializeField] private float interactDistance = 3f;  // 상호작용 가능한 거리
    private bool isMouseOverItem = false;  // 마우스가 오브젝트 위에 있는지 여부를 저장
    [SerializeField] private TextMeshProUGUI statusText;  // 상자 상태를 표시하는 TextMeshPro 텍스트
    [SerializeField] private float fadeDuration = 1f;  // 텍스트가 서서히 사라지는 시간
    [SerializeField] CanvasGroup blackoutCanvasGroup;

    [Header("Dialogue Settings")]
    public GameObject dialogueUI;  // 대화 UI 패널
    public string[] dialogues;  // 대화 내용 배열
    public TextMeshProUGUI dialogueText;  // 대화 텍스트 표시용
    private int currentDialogueIndex = 0;  // 현재 대화 인덱스
    private bool isDialogueActive = false;  // 대화가 활성화되었는지 여부

    public bool isTimeStopped = true;  // 대화 중에 시간을 멈출지 여부
    // Start is called before the first frame update
    void Start()
    {
        // 상태 텍스트 비활성화
        statusText.gameObject.SetActive(false);

        KeyManager.Instance.keyDic[KeyAction.Play] += TryEnd;  // KeyManager에서 Play 키 이벤트 등록
    }

    private void Update()
    {
        // 대화 중일 때 F키를 눌러 다음 대화로 넘어감
        if (isDialogueActive && Input.GetKeyDown(KeyCode.F))
        {
            ShowNextDialogue();
        }
    }
    // 마우스가 오브젝트 위에 있을 때 호출되는 메서드
    private void OnMouseEnter()
    {
        isMouseOverItem = true;  // 마우스가 오브젝트 위에 있음
    }

    // 마우스가 오브젝트에서 벗어날 때 호출되는 메서드
    private void OnMouseExit()
    {
        isMouseOverItem = false;  // 마우스가 오브젝트 위에 없음
    }

    // 기존 TryUpgrade 메서드 그대로 유지
    private void TryEnd()
    {
        // 플레이어와 오브젝트 사이의 거리를 계산
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // 선택된 아이템이 있으며, 일정 거리 내에 있고, 마우스가 아이템 위에 있는지 확인
        if (distanceToPlayer <= interactDistance && isMouseOverItem)
        {
            // ItemController에서 현재 보유 중인 아이템 확인
            var itemController = ItemController.Instance;

            // 인벤토리에서 11번과 5번 아이템이 있는지 확인
            var item13 = itemController.curItemDatas.Find(x => x.id == "13");

            if (item13 != null)
            {
                Debug.Log("게임 끝");
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
        ClosePanel();
    }

    void ClosePanel()
    {
        dialogueUI.SetActive(false);  // 퍼즐 패널 비활성화
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
