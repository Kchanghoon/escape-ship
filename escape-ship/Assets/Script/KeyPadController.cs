using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeypadController : MonoBehaviour
{
    public InputField inputField;     // 입력 필드
    public string correctPassword = "1234";  // 정답 비밀번호
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
}
