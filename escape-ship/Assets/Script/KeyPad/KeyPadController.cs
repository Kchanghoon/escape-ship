using UnityEngine.UI;
using UnityEngine;

public class KeypadController : MonoBehaviour
{
    [SerializeField] InputField inputField;

    private string currentInput = "";
    private KeyPad activeKeyPad;  // ���� ��ȣ�ۿ� ���� KeyPad ����

    private void Start()
    {
        inputField.text = "";
    }

    // ���� ��ȣ�ۿ� ���� KeyPad ����
    public void SetActiveKeyPad(KeyPad keyPad)
    {
        activeKeyPad = keyPad;
    }

    // ���� ��ư�� ������ �� ȣ��
    public void OnNumberButtonClick(string number)
    {
        currentInput += number;
        inputField.text = currentInput;
    }

    // ���� ��ư�� ������ �� ȣ��
    public void OnDeleteButtonClick()
    {
        if (currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
            inputField.text = currentInput;
        }
    }

    // �Է� �Ϸ� ��ư�� ������ �� ȣ��
    public void OnConfirmButtonClick()
    {
        if (activeKeyPad != null)
        {
            // ���� Ȱ��ȭ�� Ű�е�� ��й�ȣ ����
            activeKeyPad.CheckPassword(currentInput);
        }

        // �Է� �ʱ�ȭ
        currentInput = "";
        inputField.text = "";
    }

    // ��� ��ư�� ������ �� ȣ��
    public void OnCancelButtonClick()
    {
        if (activeKeyPad != null)
        {
            // KeyPad�� �г��� ��Ȱ��ȭ�ϰ� �ð� ����
            activeKeyPad.CloseKeyPad();  // activeKeyPad���� �г��� �ݴ� �Լ��� ȣ��
        }

        currentInput = "";
        inputField.text = "";
    }
}
