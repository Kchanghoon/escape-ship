using UnityEngine.UI;
using UnityEngine;

public class KeypadController : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] GameObject keyPadPanel;
    [SerializeField] Canvas keyPadCanvas;
    [SerializeField] private KeyPad keyPad;  // KeyPad ��ũ��Ʈ�� ���� ����

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
        Debug.Log("���� �Էµ� ��: " + currentInput);
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
        keyPad.CheckPassword(currentInput);  // �Էµ� ��й�ȣ�� KeyPad ��ũ��Ʈ�� ����

        // �Է� �ʱ�ȭ
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
