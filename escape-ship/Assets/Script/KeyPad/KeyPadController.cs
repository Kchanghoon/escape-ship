using UnityEngine.UI;
using UnityEngine;

public class KeypadController : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] GameObject keyPadPanel;
    [SerializeField] Canvas keyPadCanvas;
    [SerializeField] private KeyPad keyPad;  // KeyPad 스크립트에 대한 참조

    private string currentInput = "";
    private string correctPassword;

    private void Start()
    {

        inputField.text = "";
    }

    public void OnNumberButtonClick(string number)
    {
        currentInput += number;
        inputField.text = currentInput;
        Debug.Log("현재 입력된 값: " + currentInput);
    }

    public void OnDeleteButtonClick()
    {
        if (currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
            inputField.text = currentInput;
        }
    }

    public void OnConfirmButtonClick()
    {
        keyPad.CheckPassword(currentInput);  // 입력된 비밀번호를 KeyPad 스크립트로 전달

        // 입력 초기화
        currentInput = "";
        inputField.text = "";
    }

    public void OnBackButtonClick()
    {
        keyPadPanel.SetActive(false);
        Time.timeScale = 1;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        currentInput = "";
        inputField.text = "";
    }
}
