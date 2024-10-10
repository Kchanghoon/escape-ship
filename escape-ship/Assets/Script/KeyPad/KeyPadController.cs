using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeypadController : MonoBehaviour
{
    [SerializeField] InputField inputField;     // 입력 필드
    [SerializeField] string correctPassword = "1234";  // 정답 비밀번호
    [SerializeField] Canvas keyPadCanvas;  // 키패드 패널의 Canvas

    [SerializeField] GameObject keyPadPanel;  // 비밀번호 입력 패널
    private string currentInput = "";  // 현재 입력된 비밀번호


    // 숫자 버튼 클릭 시 호출
    public void OnNumberButtonClick(string number)
    {
        currentInput += number;
        inputField.text = currentInput;
        Debug.Log("현재 입력된 값: " + currentInput);  // 디버그 로그로 확인
    }
    // 지우기 버튼 클릭 시 호출
    public void OnDeleteButtonClick()
    {
        if (currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);  // 마지막 문자 제거
            inputField.text = currentInput;  // InputField 업데이트
        }
    }

    // 확인 버튼 클릭 시 호출
    public void OnConfirmButtonClick()
    {
        if (currentInput == correctPassword)
        {
            Debug.Log("비밀번호가 일치합니다.");
            // 비밀번호가 맞을 경우 수행할 로직 추가
        }
        else
        {
            Debug.Log("비밀번호가 틀렸습니다.");
            // 틀렸을 경우의 로직 추가 (예: 경고 메시지)
        }

        // 입력 초기화
        currentInput = "";
        inputField.text = "";
    }

    public void OnBackButtonClick()
    {
        // KeyPad의 ClosePlay 기능 구현
        keyPadPanel.SetActive(false);  // 패널 비활성화
        Time.timeScale = 1;  // 게임 다시 진행

        // 커서 상태 변경
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // 추가적으로 초기화가 필요하다면 추가할 수 있음 (예: 입력 초기화 등)
        currentInput = "";  // 입력 초기화
        inputField.text = "";  // 입력 필드 초기화
    }
}
