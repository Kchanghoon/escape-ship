using TMPro;
using UnityEngine;

public class KeyPad : MonoBehaviour
{
    private bool isMouseOverItem = false;
    [SerializeField] TextMeshProUGUI pickUpText;
    [SerializeField] GameObject keyPadPanel;
    [SerializeField] Transform player;
    [SerializeField] float interactDistance = 3f;
    [SerializeField] Canvas keyPadCanvas;
    [SerializeField] string correctPassword;  // 올바른 비밀번호 (개별 설정 가능)
    [SerializeField] ZDoorMotion doorMotion;  // 할당할 문 동작 (다른 오브젝트에 할당 가능)
    [SerializeField] AudioSource doorSound;  // 문이 열릴 때 재생할 사운드

    private int originalSortingOrder;

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
        keyPadPanel.SetActive(false);
        originalSortingOrder = keyPadCanvas.sortingOrder;
        KeyManager.Instance.keyDic[KeyAction.Play] += OnPlay;

        // 사운드가 없을 경우 에디터에서 설정되지 않았다고 알림
        if (doorSound == null)
        {
            Debug.LogWarning("AudioSource가 할당되지 않았습니다. 사운드가 재생되지 않습니다.");
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (isMouseOverItem && distanceToPlayer <= interactDistance)
        {
            pickUpText.gameObject.SetActive(true);
            pickUpText.text = "E키를 눌러 비밀번호 패드를 여세요";
        }
        else
        {
            pickUpText.gameObject.SetActive(false);
        }
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
        if (isMouseOverItem)
        {
            keyPadPanel.SetActive(true);
            keyPadCanvas.sortingOrder = 999;
            Time.timeScale = 0;
            MouseCam mouseCam = FindObjectOfType<MouseCam>();
            if (mouseCam != null)
            {
                mouseCam.UnlockCursor();
            }
        }
    }

    // 비밀번호 확인 함수
    public void CheckPassword(string inputPassword)  // 매개변수로 입력된 비밀번호 받기
    {
        if (inputPassword == correctPassword)  // 전달받은 비밀번호가 맞는지 확인
        {
            Debug.Log("비밀번호가 맞았습니다. 문을 엽니다.");

            // 문을 열기 전에 사운드 재생
            if (doorSound != null)
            {
                doorSound.Play();  // 사운드 재생
            }

            doorMotion.OpenDoor();  // 문 열기
            CloseKeyPad();  // 패널 닫기
        }
        else
        {
            Debug.Log("비밀번호가 틀렸습니다.");
        }
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
