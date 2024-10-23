using System;
using UnityEngine;
using UnityEngine.UI;  // Button Ŭ���� ����� ���� �ʿ�
using DG.Tweening;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject StartMainUI;
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject confirmExitPanel; // ���� Ȯ��â UI
    [SerializeField] private GameObject confirmOptionSlotPanel; // �ɼ�â UI
    [SerializeField] private GameObject confirmLoadSlotPanel; // �ε� UI
    [SerializeField] private GameObject confirmSelectSlotPanel; // �������� ���� UI
    [SerializeField] private Button resumeButton;  // Resume ��ư

    private bool isPaused = false;
    public GameObject panel;

    private void Start()
    {
        // ���콺 Ŀ�� �ʱ�ȭ
        MouseCam.Instance.SetCursorState(false);

        // ����� ���� ���°� �ִ��� Ȯ���Ͽ� Resume ��ư Ȱ��ȭ/��Ȱ��ȭ
        CheckSavedGame();
    }

    // ����� ������ �ִ��� Ȯ���ϴ� �޼���
    private void CheckSavedGame()
    {
        // ���÷� PlayerPrefs ���. ���� ���ӿ����� ���̺� �����̳� DB Ȯ�� ����
        if (PlayerPrefs.HasKey("SavedGame"))
        {
            resumeButton.interactable = true;  // ����� ������ ������ Resume ��ư Ȱ��ȭ
        }
        else
        {
            resumeButton.interactable = false;  // ����� ������ ������ Resume ��ư ��Ȱ��ȭ
        }
    }

    // �������� ���� �� StageManager�� �����Ͽ� Ȱ��ȭ
    public void SelectStage(int stageIndex)
    {
        Resume(); // ���� �� �޴��� ����
        StageManager.Instance.ActivateStage(stageIndex); // StageManager�� �޼��带 ȣ��
    }

    public void Update()
    {
        if (StartMainUI.activeSelf || PauseMenuUI.activeSelf)
        {
            // �޴� BGM ���� ó���ϴ� �ڵ�
        }
    }

    public void OnPanelClick()
    {
        panel.transform.SetAsLastSibling(); // �г��� ���� ������ ����
    }

    public void Resume()
    {
        StartMainUI.SetActive(false);
        PauseMenuUI.SetActive(false);
        confirmExitPanel.SetActive(false);
        confirmOptionSlotPanel.SetActive(false);
        confirmLoadSlotPanel.SetActive(false);
        confirmSelectSlotPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
        MouseCam.Instance.SetCursorState(true);  // ���콺 Ŀ�� ���
    }

    public void showConfirmExitPanel() { confirmExitPanel.SetActive(true); }
    public void showLoadSlotPanel() { confirmLoadSlotPanel.SetActive(true); }
    public void showOptionPanel() { confirmOptionSlotPanel.SetActive(true); }
    public void showStageSelectPanel() { confirmSelectSlotPanel.SetActive(true); }
    public void CancelExit() { confirmExitPanel.SetActive(false); confirmLoadSlotPanel.SetActive(false); confirmOptionSlotPanel.SetActive(false); confirmSelectSlotPanel.SetActive(false); }

    public void QuitGame()
    {
        // ���� ���� ���
        Application.Quit();

        // ������ ��忡�� �׽�Ʈ ���� ���
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
