using UnityEngine;
using TMPro;  // TextMeshPro 라이브러리 추가

public class DialogueTriggerUI : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public GameObject dialogueUI;  // 표시할 대화 UI (패널)
    public string[] dialogues;  // 대화 내용 배열
    public bool isTimeStopped = true;  // 대화 중에 시간을 멈출지 여부
    private bool isDialogueActive = false;
    private int currentDialogueIndex = 0;  // 현재 대화 인덱스
    private bool hasTriggered = false;  // 대화가 이미 한번 발생했는지 여부

    [Header("Player Settings")]
    public string playerTag = "Player";  // 플레이어 태그, 기본값은 "Player"
    private Transform playerTransform;  // 플레이어 Transform을 저장
    public float interactionDistance = 3f;  // 플레이어와 상호작용할 수 있는 거리

    [Header("Dialogue Text Reference")]
    public TextMeshProUGUI dialogueText;  // 대화 내용을 표시할 TextMeshProUGUI

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;  // 플레이어의 Transform 가져오기
        dialogueUI.SetActive(false);  // 시작할 때 UI를 비활성화
    }

    private void Update()
    {
        // 대화 중일 때 F키를 눌러 다음 대화로 넘어감
        if (isDialogueActive && Input.GetKeyDown(KeyCode.F))
        {
            ShowNextDialogue();
        }
    }

    // 플레이어가 특정 구역에 들어갈 때 트리거 발생
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !isDialogueActive && !hasTriggered)  // 대화가 한 번도 발생하지 않았을 때만 실행
        {
            float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

            if (distanceToPlayer <= interactionDistance)
            {
                ShowDialogue();  // 대화창 표시
                hasTriggered = true;  // 대화가 발생했음을 기록
            }
        }
    }

    // 대화창을 표시하는 메서드
    void ShowDialogue()
    {
        dialogueUI.SetActive(true);  // UI를 활성화
        currentDialogueIndex = 0;  // 대화 시작 시 인덱스를 0으로 설정
        ShowNextDialogue();  // 첫 대화 표시

        if (isTimeStopped)
        {
            Time.timeScale = 0f;  // 시간을 멈춤
        }

        isDialogueActive = true;
    }

    // 대화창에서 대화 내용을 표시하고, 마지막 대화면 창을 닫음
    void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];  // 현재 대화 내용을 UI에 표시
            currentDialogueIndex++;  // 다음 대화를 준비
        }
        else
        {
            CloseDialogue();  // 대화가 끝났을 때 창 닫기
        }
    }

    // 대화가 종료되면 UI를 숨기고 시간을 재개하는 메서드
    public void CloseDialogue()
    {
        dialogueUI.SetActive(false);  // UI를 비활성화

        if (isTimeStopped)
        {
            Time.timeScale = 1f;  // 시간을 다시 흐르게 함
        }

        isDialogueActive = false;
    }
}
