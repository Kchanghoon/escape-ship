using System;
using UnityEngine;
using DG.Tweening;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject StartMenuUI;
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject confirmExitPanel; // ���� Ȯ��â UI
    [SerializeField] private GameObject confirmOptionSlotPanel; // �ɼ�â UI
    [SerializeField] private GameObject confirmLoadSlotPanel; // �ε� UI
    [SerializeField] private GameObject confirmSelectSlotPanel; // �������� ���� UI
    private bool isPaused = false;
    public GameObject panel;

    // �������� ���� �� StageManager�� �����Ͽ� Ȱ��ȭ
    public void SelectStage(int stageIndex)
    {
        Resume(); // ���� �� �޴��� ����
        // StageManager���� �ش� ���������� Ȱ��ȭ
        StageManager.Instance.ActivateStage(stageIndex); // StageManager�� �޼��带 ȣ��
       
    }

    public void Update()
    {
        if (StartMenuUI.activeSelf || PauseMenuUI.activeSelf)
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
        StartMenuUI.SetActive(false);
        confirmExitPanel.SetActive(false);
        confirmOptionSlotPanel.SetActive(false);
        confirmLoadSlotPanel.SetActive(false);
        confirmSelectSlotPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
