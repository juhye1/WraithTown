using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel_GameOver : MonoBehaviour
{
    public TextMeshProUGUI resultTMP;

    private void OnEnable()
    {
        WTMain main = WTMain.Instance;
        if(main.GetCurrentStageData().stage_id != 10006)
        {
            resultTMP.SetText("DEFEAT");
        }
        else
        {
            resultTMP.SetText("VICTORY");
        }
    }
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


}
