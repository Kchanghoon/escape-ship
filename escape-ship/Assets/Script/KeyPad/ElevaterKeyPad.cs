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
    [SerializeField] CanvasGroup blackoutCanvasGroup;
    [SerializeField] float fadeDuration = 2f;
    [SerializeField] float elevatorMoveDuration = 3f;

    private int originalSortingOrder;

    private void Start()
    {
        keyPadPanel.SetActive(false);
        originalSortingOrder = keyPadCanvas.sortingOrder;
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;

        if (doorSound == null)
        {
            Debug.LogWarning("AudioSource가 할당되지 않았습니다. 사운드가 재생되지 않습니다.");
        }

        if (elevatorMoveSound == null)
        {
            Debug.LogWarning("엘리베이터 이동 소리가 할당되지 않았습니다.");
        }

        if (blackoutCanvasGroup != null)
        {
            blackoutCanvasGroup.alpha = 0;
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
        Time.timeScale = 0;

        keyPadController.SetActiveElevaterKeyPad(this); // ElevaterKeyPad를 설정

        MouseCam mouseCam = FindObjectOfType<MouseCam>();
        if (mouseCam != null)
        {
            mouseCam.UnlockCursor();
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

            doorMotion.CloseDoor();
            StartCoroutine(MoveElevator());
            CloseKeyPad();  // 키패드 닫기
            doorMotion.OpenDoor();
        }
        else
        {
            Debug.Log("비밀번호가 틀렸습니다.");
        }
    }

    private IEnumerator MoveElevator()
    {
        if (elevatorMoveSound != null)
        {
            elevatorMoveSound.Play();
        }

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

        yield return new WaitForSeconds(elevatorMoveDuration);
    }

    public void CloseKeyPad()
    {
        keyPadPanel.SetActive(false);
        keyPadCanvas.sortingOrder = originalSortingOrder;
        Time.timeScale = 1;

        MouseCam mouseCam = FindObjectOfType<MouseCam>();
        if (mouseCam != null)
        {
            mouseCam.LockCursor();
        }
    }
}
