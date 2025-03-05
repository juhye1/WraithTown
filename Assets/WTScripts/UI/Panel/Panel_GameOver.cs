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
        if(main.GetCurrentStageData().stage_id != 10005)
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
        Application.Quit();
        //main.ChangeGameState(WTGameState.Lobby);
        //gameObject.SetActive(false);
        //로비로
    }

    public void OnClickBtn_Restart()
    {
        WTMain main = WTMain.Instance;
        main.gameMode = WTGameMode.Restart;
        main.RestartGame();
        gameObject.SetActive(false);

        //재시작
    }


}
