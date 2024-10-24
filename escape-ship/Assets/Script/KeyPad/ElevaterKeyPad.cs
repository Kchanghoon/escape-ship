using System.Collections;
using UnityEngine;

public class ElevaterKeyPad : MonoBehaviour
{
    private bool isMouseOverItem = false;
    [SerializeField] GameObject keyPadPanel;
    [SerializeField] Transform player;
    [SerializeField] float interactDistance = 3f;
    [SerializeField] Canvas keyPadCanvas;
    [SerializeField] string correctPassword;
    [SerializeField] ZDoorMotion doorMotion;
    [SerializeField] AudioSource doorSound;
    [SerializeField] AudioSource elevatorMoveSound;
    [SerializeField] KeypadController keyPadController;
    [SerializeField] float elevatorMoveDuration = 3f;
    [SerializeField] StageManager stageManager;  // StageManager 인스턴스 참조

    [SerializeField] BlackOutChange blackOutChange;  // BlackOutChange 스크립트 참조
    private int originalSortingOrder;

    private void Start()
    {
        // StageManager 인스턴스 가져오기
        stageManager = StageManager.Instance;

        keyPadPanel.SetActive(false);
        originalSortingOrder = keyPadCanvas.sortingOrder;

        // KeyAction.Play 이벤트에 OnPlay 메서드 연결
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;

        if (doorSound == null)
        {
            Debug.LogWarning("AudioSource가 할당되지 않았습니다. 사운드가 재생되지 않습니다.");
        }

        if (elevatorMoveSound == null)
        {
            Debug.LogWarning("엘리베이터 이동 소리가 할당되지 않았습니다.");
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
    }

    private void OnMouseEnter()
    {
        isMouseOverItem = true;
    }

    private void OnMouseExit()
    {
        isMouseOverItem = false;
    }

    // 플레이어가 Play 키를 눌렀을 때 호출될 메서드
    public void OnPlay()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (isMouseOverItem && distanceToPlayer <= interactDistance)
        {
            OpenKeyPadPanel();
        }
    }

    private void OpenKeyPadPanel()
    {
        keyPadPanel.SetActive(true);
        keyPadCanvas.sortingOrder = 999;

        Time.timeScale = 0;  // 시간 정지 (게임 일시정지)
        keyPadController.SetActiveElevaterKeyPad(this); // ElevaterKeyPad를 설정

        MouseCam mouseCam = FindObjectOfType<MouseCam>();
        if (mouseCam != null)
        {
            mouseCam.SetCursorState(false);  // 커서를 잠금 해제
        }
    }

    public void CheckPassword(string inputPassword)
    {
        if (inputPassword == correctPassword)
        {
            Debug.Log("비밀번호가 맞습니다. 엘리베이터가 이동합니다.");

            if (doorSound != null)
            {
                doorSound.Play();
            }

            // 키패드를 먼저 닫고 블랙아웃 및 엘리베이터 이동 처리
            CloseKeyPad();
            StartCoroutine(HandleElevatorTransition());
            doorMotion.CloseDoor();
        }
        else
        {
            Debug.Log("비밀번호가 틀렸습니다.");
        }
    }

    // 블랙아웃과 엘리베이터 이동 처리
    private IEnumerator HandleElevatorTransition()
    {        // 엘리베이터 이동 소리 재생
        if (elevatorMoveSound != null)
        {
            elevatorMoveSound.Play();
        }
        // 블랙아웃 시작
        if (blackOutChange != null)
        {
            yield return StartCoroutine(blackOutChange.StartBlackOut());
        }

        doorMotion.CloseDoor();

        // 블랙아웃 종료
        if (blackOutChange != null)
        {
            yield return StartCoroutine(blackOutChange.EndBlackOut());
        }
        stageManager.ActivateStage(2);

    }

    public void CloseKeyPad()
    {
        // 키패드를 코루틴 실행 전에 먼저 닫음
        keyPadPanel.SetActive(false);
        keyPadCanvas.sortingOrder = originalSortingOrder;

        Time.timeScale = 1;  // 시간 재개 (게임 일시정지 해제)
        MouseCam mouseCam = FindObjectOfType<MouseCam>();
        if (mouseCam != null)
        {
            mouseCam.SetCursorState(true);  // 커서를 잠금
        }
    }

    private void OnDestroy()
    {
        // OnDestroy에서 이벤트를 해제하여 메모리 누수 방지
        KeyManager.Instance.keyDic[KeyAction.Play] -= OnPlay;
    }
}
