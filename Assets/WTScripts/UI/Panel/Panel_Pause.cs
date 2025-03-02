using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Pause : MonoBehaviour
{

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
    public void OnClickBtn_Save()
    {
        WTMain main = WTMain.Instance;
        main.SaveData();
    }

    public void OnClickBtn_Continue()
    {
        gameObject.SetActive(false);
    }

    public void OnClickBtn_Restart()
    {
        WTMain main = WTMain.Instance;
        main.RestartGame();
        gameObject.SetActive(false);
    }

    public void OnClickBtn_Option()
    {
        //사운드 설정
    }

    public void OnClickBtn_Exit()
    {
        Application.Quit();
    }
}
