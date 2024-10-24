using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EndKeypadController : MonoBehaviour
{
    [SerializeField] InputField inputField;     // �Է� �ʵ�
    [SerializeField] string correctPassword = "1234";  // ���� ��й�ȣ
    [SerializeField] Canvas keyPadCanvas;  // Ű�е� �г��� Canvas

    [SerializeField] GameObject keyPadPanel;  // ��й�ȣ �Է� �г�
    private string currentInput = "";  // ���� �Էµ� ��й�ȣ
    private StageManager stageManager;  // StageManager �ν��Ͻ� ����
    private BlackOutChange blackOutChange;  // BlackOutChange ��ũ��Ʈ ����

    private void Start()
    {
        // StageManager �ν��Ͻ� ��������
        stageManager = StageManager.Instance;

        // BlackOutChange ��ũ��Ʈ ����
        blackOutChange = FindObjectOfType<BlackOutChange>();
    }

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
            // ��й�ȣ�� ���� ��� Stage1���� �̵�
            StartCoroutine(HandleStageTransition());  // ���ƿ��� �Բ� �������� ��ȯ
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

    // ���ƿ��� �Բ� �������� ��ȯ ó��
    private IEnumerator HandleStageTransition()
    {
        // Ű�е� �ݱ�
        OnBackButtonClick();

        // ���ƿ� ����
        if (blackOutChange != null)
        {
            yield return StartCoroutine(blackOutChange.StartBlackOut());
        }

        // �������� 1�� �̵�
        stageManager.ActivateStage(1);

        // ���� �ð� ��� �� ���ƿ� ��
        if (blackOutChange != null)
        {
            yield return StartCoroutine(blackOutChange.EndBlackOut());
        }
    }

    public void OnBackButtonClick()
    {
        // KeyPad�� ClosePlay ��� ����
        keyPadPanel.SetActive(false);  // �г� ��Ȱ��ȭ
        Time.timeScale = 1;  // ���� �ٽ� ����

        // Ŀ�� ���� ����
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // �߰������� �ʱ�ȭ�� �ʿ��ϴٸ� �߰��� �� ���� (��: �Է� �ʱ�ȭ ��)
        currentInput = "";  // �Է� �ʱ�ȭ
        inputField.text = "";  // �Է� �ʵ� �ʱ�ȭ
    }
}
