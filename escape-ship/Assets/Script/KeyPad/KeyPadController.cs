using UnityEngine.UI;  // UnityEngine.UI 네임스페이스는 UI 요소(예: InputField)와 작업하는 데 필요
using UnityEngine;     // Unity의 기본 네임스페이스

public class KeypadController : MonoBehaviour
{
    [SerializeField] InputField inputField;  // 사용자가 입력한 숫자를 표시하는 UI InputField

    private string currentInput = "";        // 현재 입력 중인 숫자를 저장
    private KeyPad activeKeyPad;             // 활성화된 일반 KeyPad 인스턴스
    private ElevaterKeyPad activeElevaterKeyPad;  // 활성화된 ElevaterKeyPad 인스턴스
    private BoxKeyPad activeBoxKeyPad;       // 추가된 BoxKeyPad 타입의 활성화된 KeyPad 인스턴스

    private void Start()
    {
        // 게임 시작 시 InputField의 텍스트를 초기화
        inputField.text = "";
    }

    // 일반 KeyPad를 활성화하는 메서드
    public void SetActiveKeyPad(KeyPad keyPad)
    {
        activeKeyPad = keyPad;  // 지정된 KeyPad를 활성화
        activeElevaterKeyPad = null;  // 다른 KeyPad 타입 비활성화
        activeBoxKeyPad = null;       // 다른 KeyPad 타입 비활성화
    }

    // ElevaterKeyPad를 활성화하는 메서드
    public void SetActiveElevaterKeyPad(ElevaterKeyPad elevaterKeyPad)
    {
        activeElevaterKeyPad = elevaterKeyPad;  // 지정된 ElevaterKeyPad를 활성화
        activeKeyPad = null;  // 다른 KeyPad 타입 비활성화
        activeBoxKeyPad = null;  // 다른 KeyPad 타입 비활성화
    }

    // BoxKeyPad를 활성화하는 메서드
    public void SetActiveBoxKeyPad(BoxKeyPad boxKeyPad)
    {
        activeBoxKeyPad = boxKeyPad;  // 지정된 BoxKeyPad를 활성화
        activeKeyPad = null;  // 다른 KeyPad 타입 비활성화
        activeElevaterKeyPad = null;  // 다른 KeyPad 타입 비활성화
    }

    // 숫자 버튼이 클릭되었을 때 호출
    public void OnNumberButtonClick(string number)
    {
        currentInput += number;  // 현재 입력 문자열에 새 숫자를 추가
        inputField.text = currentInput;  // InputField에 업데이트된 입력값 표시
    }

    // 삭제 버튼이 클릭되었을 때 호출
    public void OnDeleteButtonClick()
    {
        if (currentInput.Length > 0)  // 현재 입력값이 비어있지 않은 경우
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);  // 마지막 문자를 제거
            inputField.text = currentInput;  // InputField에 업데이트된 입력값 표시
        }
    }

    // 확인 버튼이 클릭되었을 때 호출
    public void OnConfirmButtonClick()
    {
        // 활성화된 KeyPad에 대해 입력값 확인 요청
        if (activeKeyPad != null)
        {
            activeKeyPad.CheckPassword(currentInput);  // 일반 KeyPad에 입력값 전달
        }
        else if (activeElevaterKeyPad != null)
        {
            activeElevaterKeyPad.CheckPassword(currentInput);  // ElevaterKeyPad에 입력값 전달
        }
        else if (activeBoxKeyPad != null)
        {
            activeBoxKeyPad.CheckPassword(currentInput);  // BoxKeyPad에 입력값 전달
        }

        // 입력값 초기화
        currentInput = "";  // 현재 입력값 초기화
        inputField.text = "";  // InputField의 텍스트 초기화
    }

    // 취소 버튼이 클릭되었을 때 호출
    public void OnCancelButtonClick()
    {
        // 활성화된 KeyPad에 대해 닫기 요청
        if (activeKeyPad != null)
        {
            activeKeyPad.CloseKeyPad();  // 일반 KeyPad 닫기
        }
        else if (activeElevaterKeyPad != null)
        {
            activeElevaterKeyPad.CloseKeyPad();  // ElevaterKeyPad 닫기
        }
        else if (activeBoxKeyPad != null)
        {
            activeBoxKeyPad.CloseKeyPad();  // BoxKeyPad 닫기
        }

        // 입력값 초기화
        currentInput = "";  // 현재 입력값 초기화
        inputField.text = "";  // InputField의 텍스트 초기화
    }
}
