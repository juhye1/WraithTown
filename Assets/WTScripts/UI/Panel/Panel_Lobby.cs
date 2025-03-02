using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Lobby : MonoBehaviour
{
    public Image continueDisableImg;
    private void OnEnable()
    {
        WTGlobal.RegisterEventDelegate(WTEventType.SaveDataLoaded, EnableContinueBtn);
    }
    private void OnDisable()
    {
        WTGlobal.UnregisterEventDelegate(WTEventType.SaveDataLoaded, EnableContinueBtn);
    }
    private void EnableContinueBtn(int val)
    {
        bool enable = val == 1 ? true : false;
        continueDisableImg.enabled = enable;
    }
    public void OnClickBtn_StartGame()
    {
        WTMain main = WTMain.Instance; // 나중에 스토리로 바꿔야해
        main.ChangeGameState(WTGameState.SelectCharacter);
    }
    public void OnClickBtn_ContinueGame()
    {
        WTMain main = WTMain.Instance;
        main.ChangeGameState(WTGameState.Game);
    }
    public void OnClickBtn_QuitGame()
    {
        Application.Quit();
    }
    public void OnClickBtn_OptionMenu()
    {
        WTMain main = WTMain.Instance;
        main.SaveData();
    }
}
