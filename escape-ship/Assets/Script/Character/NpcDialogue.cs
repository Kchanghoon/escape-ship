using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;  // NPC와 연결된 대화 패널
    public TextMeshProUGUI dialogueText;  // 대화 내용 표시할 텍스트
    public string[] dialogues;  // NPC 대화 내용들
    public float interactionDistance = 3f;  // NPC와 상호작용할 수 있는 거리
    private Transform playerTransform;  // 플레이어의 Transform
    private bool isMouseOverNPC = false;  // 마우스가 NPC 위에 있는지 여부
    private bool isDialogueActive = false;  // 대화 패널 상태
    private int currentDialogueIndex = 0;  // 현재 대화 인덱스
    private Canvas dialogueCanvas;  // 대화 패널의 Canvas
    public TextMeshProUGUI TalktoText;  // 화면에 출력할 텍스트 (TextMeshPro 사용 시)

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // 플레이어의 Transform 가져오기
        dialoguePanel.SetActive(false);  // 대화 패널 처음엔 비활성화
                                         // 대화 패널의 Canvas 가져오기
        dialogueCanvas = dialoguePanel.GetComponentInParent<Canvas>();
        TalktoText.gameObject.SetActive(false);  // 처음엔 텍스트를 비활성화
        // 키 입력 이벤트를 KeyManager의 keyDic에 등록 (PlayAction에 연결)
        KeyManager.Instance.keyDic[KeyAction.Play] += TryToggleDialoguePanel;

        // KeyAction.NextDialogue로 대화를 넘길 수 있게 연결
        KeyManager.Instance.keyDic[KeyAction.PickUp] += ShowNextDialogue;
    }

    // 마우스가 NPC 위에 있을 때 호출
    private void OnMouseEnter()
    {
        isMouseOverNPC = true;  // 마우스가 NPC 위에 있음
        TalktoText.gameObject.SetActive(true);  // 텍스트 활성화
        TalktoText.text = "E키를 눌러 대화하세요";  // 문구 설정
    }

    // 마우스가 NPC에서 벗어났을 때 호출
    private void OnMouseExit()
    {
        isMouseOverNPC = false;  // 마우스가 NPC에서 벗어남
        TalktoText.gameObject.SetActive(false);  // 에임이 벗어나면 텍스트 비활성화
    }

    // 키 입력으로 대화 패널을 여는 함수
    private void TryToggleDialoguePanel()
    {
        // 마우스가 NPC 위에 있고, 플레이어가 가까운 경우에만 실행
        if (isMouseOverNPC)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);

            if (distance <= interactionDistance)
            {
                ToggleDialoguePanel();
            }
            else
            {
                TalktoText.gameObject.SetActive(false);  // 거리가 멀어지면 텍스트 비활성화
            }
        }
    }

    // 패널을 열거나 닫는 함수
    private void ToggleDialoguePanel()
    {
        isDialogueActive = !isDialogueActive;  // 패널 상태 토글
        dialoguePanel.SetActive(isDialogueActive);  // 패널 활성화/비활성화

        if (isDialogueActive)
        {
            currentDialogueIndex = 0;  // 대화를 처음부터 시작
            ShowNextDialogue();

            // 시간을 멈춤
            Time.timeScale = 0f;
            // Canvas의 Sorting Order를 100으로 설정 (값을 원하는 만큼 높게 설정)
            dialogueCanvas.sortingOrder = 100;
        }
        else
        {
            // 대화가 끝나면 시간을 다시 정상으로 돌림
            Time.timeScale = 1f;
            dialogueCanvas.sortingOrder = 0;
        }
    }

    // 대화 내용 표시
    private void ShowNextDialogue()
    {
        if (isDialogueActive)  // 대화가 활성화되어 있을 때만 대화 진행
        {
            if (currentDialogueIndex < dialogues.Length)
            {
                dialogueText.text = dialogues[currentDialogueIndex];  // 대화 내용 설정
                currentDialogueIndex++;
            }
            else
            {
                ToggleDialoguePanel();  // 대화가 끝나면 패널 닫기
            }
        }
    }
}
