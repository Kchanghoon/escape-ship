using UnityEngine.UI;
using UnityEngine;

public class KeypadController : MonoBehaviour
{
    [SerializeField] InputField inputField;

    private string currentInput = "";
    private KeyPad activeKeyPad;  // ���� ��ȣ�ۿ� ���� KeyPad ����
    private ElevaterKeyPad activeElevaterKeyPad;  // ���� ��ȣ�ۿ� ���� ElevaterKeyPad ����
    private BoxKeyPad activeBoxKeyPad;  // 추가된 BoxKeyPad 저장
    
    private void Start()
    {
        inputField.text = "";
    }

   // 일반 KeyPad를 활성화하는 메서드
    public void SetActiveKeyPad(KeyPad keyPad)
    {
        activeKeyPad = keyPad;
        activeElevaterKeyPad = null;
        activeBoxKeyPad = null;  // 다른 KeyPad 타입을 비활성화
    }

    // ElevaterKeyPad를 활성화하는 메서드
    public void SetActiveElevaterKeyPad(ElevaterKeyPad elevaterKeyPad)
    {
        activeElevaterKeyPad = elevaterKeyPad;
        activeKeyPad = null;
        activeBoxKeyPad = null;  // 다른 KeyPad 타입을 비활성화
    }

    // BoxKeyPad를 활성화하는 메서드
    public void SetActiveBoxKeyPad(BoxKeyPad boxKeyPad)
    {
        activeBoxKeyPad = boxKeyPad;
        activeKeyPad = null;
        activeElevaterKeyPad = null;  // 다른 KeyPad 타입을 비활성화
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
        // KeyPad�� Ȱ��ȭ�Ǿ� ������ �������� ��й�ȣ ����
        if (activeKeyPad != null)
        {
            activeKeyPad.CheckPassword(currentInput);
        }
        // ElevaterKeyPad�� Ȱ��ȭ�Ǿ� ������ �������� ��й�ȣ ����
        else if (activeElevaterKeyPad != null)
        {
            activeElevaterKeyPad.CheckPassword(currentInput);
        }
        else if (activeBoxKeyPad != null)
        {
            activeBoxKeyPad.CheckPassword(currentInput);
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
            activeKeyPad.CloseKeyPad(); // KeyPad���� �г��� �ݴ� �Լ��� ȣ��
        }
        else if (activeElevaterKeyPad != null)
        {
            activeElevaterKeyPad.CloseKeyPad(); // ElevaterKeyPad���� �г��� �ݴ� �Լ��� ȣ��
        }
        else if (activeBoxKeyPad != null)
        {
            activeBoxKeyPad.CloseKeyPad(); 
        }
        currentInput = "";
        inputField.text = "";
    }
}

