using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeypadController : MonoBehaviour
{
    public InputField inputField;     // �Է� �ʵ�
    public string correctPassword = "1234";  // ���� ��й�ȣ
    private string currentInput = "";  // ���� �Էµ� ��й�ȣ

    // ���� ��ư Ŭ�� �� ȣ��
    public void OnNumberButtonClick(string number)
    {
        currentInput += number;
        inputField.text = currentInput;
        Debug.Log("���� �Էµ� ��: " + currentInput);  // ����� �α׷� Ȯ��
    }
    // ����� ��ư Ŭ�� �� ȣ��
    public void OnDeleteButtonClick()
    {
        if (currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);  // ������ ���� ����
            inputField.text = currentInput;  // InputField ������Ʈ
        }
    }

    // Ȯ�� ��ư Ŭ�� �� ȣ��
    public void OnConfirmButtonClick()
    {
        if (currentInput == correctPassword)
        {
            Debug.Log("��й�ȣ�� ��ġ�մϴ�.");
            // ��й�ȣ�� ���� ��� ������ ���� �߰�
        }
        else
        {
            Debug.Log("��й�ȣ�� Ʋ�Ƚ��ϴ�.");
            // Ʋ���� ����� ���� �߰� (��: ��� �޽���)
        }

        // �Է� �ʱ�ȭ
        currentInput = "";
        inputField.text = "";
    }
}
