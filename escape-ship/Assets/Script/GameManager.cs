using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isPause;
    public bool isSetting;

    public void SetPause(bool isPause)
    {
        this.isPause = isPause;
        UpdateGameState();
    }

    public void SetSetting(bool isSetting)
    {
        this.isSetting = isSetting;
    }

    private void UpdateGameState()
    {
        // 일시정지 또는 설정 메뉴가 활성화되었을 때
        if (isPause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
