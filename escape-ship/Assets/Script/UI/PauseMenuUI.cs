using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class PauseMenuUI : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject BasepauseMenuUI;
    [SerializeField] private GameObject confirmMenuPanel;
    [SerializeField] private GameObject confirmOptionPanel;
    [SerializeField] private GameObject confirmExitPanel;
    [SerializeField] private GameObject MainMenuUI;

    [Header("Audio Settings")]

    [SerializeField] AudioSource BTNClick;  // 문이 열릴 때 재생할 소리

    [Header("Other Settings")]
    [SerializeField] private GameObject[] uiElements;

    private bool isPause;

    private void Start()
    {
        KeyManager.Instance.keyDic[KeyAction.Setting] += OnSetting;
    }


    private void OnSetting()
    {
        // 메인 메뉴 UI가 활성화되어 있으면 아무 작업도 하지 않고 종료
        if (MainMenuUI.activeInHierarchy)
        {
            return;
        }

        if (isPause == false) // 게임이 일시정지 상태가 아니고, 다른 UI가 활성화되지 않은 경우에만 실행
        {
            Show();
            GameManager.Instance.isSetting = true;
            BasepauseMenuUI.SetActive(true);
            isPause = true;
        }
        else // 게임이 일시정지 상태이며, 다른 UI가 활성화된 경우에만 실행
        {
            Hide();
            GameManager.Instance.isSetting = false;
            BasepauseMenuUI.SetActive(false);
            confirmOptionPanel.SetActive(false);
            confirmExitPanel.SetActive(false);
            confirmMenuPanel.SetActive(false);
            isPause = false;
        }
    }


    private void Show()
    {
        GameManager.Instance.ShowMouse();
    }

    private void Hide()
    {
        GameManager.Instance.HideMouse();
    }



    public void showConfirmMenuPanel()
    {
        confirmMenuPanel.SetActive(true);
        BTNClick.Play();
    }

    public void showConfirmOptionPanel()
    {
        confirmOptionPanel.SetActive(true);
        BTNClick.Play();
    }

    public void showConfirmExitPanel()
    {
        confirmExitPanel.SetActive(true);
        BTNClick.Play();
    }


    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.ShowMouse();
        //GameManager.Instance.SetPause(false);
        BTNClick.Play();
        BasepauseMenuUI.SetActive(false);
        confirmMenuPanel.SetActive(false);
        confirmExitPanel.SetActive(false);
        confirmOptionPanel.SetActive(false);
        MainMenuUI.SetActive(true);

    }

    public void CancelExit()
    {
        confirmMenuPanel.SetActive(false);
        confirmExitPanel.SetActive(false);
        BTNClick.Play();
    }

    public void QuitGame()
    {
        BTNClick.Play();
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

 

}
