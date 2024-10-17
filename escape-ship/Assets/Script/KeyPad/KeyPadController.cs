using UnityEngine.UI;
using UnityEngine;

public class KeypadController : MonoBehaviour
{
    [SerializeField] InputField inputField;

    private string currentInput = "";
    private KeyPad activeKeyPad;  // 현재 상호작용 중인 KeyPad 참조

    private void Start()
    {
        inputField.text = "";
    }

    // 현재 상호작용 중인 KeyPad 설정
    public void SetActiveKeyPad(KeyPad keyPad)
    {
        activeKeyPad = keyPad;
    }

    // 숫자 버튼을 눌렀을 때 호출
    public void OnNumberButtonClick(string number)
    {
        currentInput += number;
        inputField.text = currentInput;
    }

    // 삭제 버튼을 눌렀을 때 호출
    public void OnDeleteButtonClick()
    {
        if (currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
            inputField.text = currentInput;
        }
    }

    // 입력 완료 버튼을 눌렀을 때 호출
    public void OnConfirmButtonClick()
    {
        if (activeKeyPad != null)
        {
            // 현재 활성화된 키패드로 비밀번호 전송
            activeKeyPad.CheckPassword(currentInput);
        }

        // 입력 초기화
        currentInput = "";
        inputField.text = "";
    }

    // 취소 버튼을 눌렀을 때 호출
    public void OnCancelButtonClick()
    {
        if (activeKeyPad != null)
        {
            // KeyPad의 패널을 비활성화하고 시간 복원
            activeKeyPad.CloseKeyPad();  // activeKeyPad에서 패널을 닫는 함수를 호출
        }

        currentInput = "";
        inputField.text = "";
    }
}
