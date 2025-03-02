using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_GameOver : MonoBehaviour
{
    public void OnClickBtn_GotoLobby()
    {
        WTMain main = WTMain.Instance;
        main.ChangeGameState(WTGameState.Lobby);
        //로비로
    }

    public void OnClickBtn_Restart()
    {
        WTMain main = WTMain.Instance;
        main.RestartGame();

        //재시작
    }

    private void OnESC()
    {
        //이따 ESC 키에 붙이기
        WTUIMain uiMain = WTUIMain.Instance;
        uiMain.GetPanel(WTUIState.Pause);
    }

}
