using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class WTMain : MonoBehaviour
{
    private static WTMain _instance;
    public static WTMain Instance
    {
        get
        {
            return _instance;
        }
    }

    [NonSerialized] public int[] sortLayerID = new int[(int)ESortLayer.Count];
    [NonSerialized] public WTGameState gameState = WTGameState.None;
    [NonSerialized] public WTGameState befGameState = WTGameState.None;
    [NonSerialized] public WTGameMode gameMode = WTGameMode.None;
    public bool isTestMode = true;

    public void Awake()
    {
        _instance = this;
        InitDatas();

    }
    private void Start()
    {
        if(isTestMode)
        {
            ChangeGameState(WTGameState.Lobby);
        }
    }
    private void Update()
    {
        float dt = Time.deltaTime;
        UpdateTimer(dt);
    }
    public bool ChangeGameState(WTGameState eState) 
    {
        //Debug.Log("ChangeGameState - " + eState);

        if (eState == gameState) return true;

        WTUIMain uiMain = WTUIMain.Instance;
        befGameState = gameState;

        gameState = eState;
        if (gameState == WTGameState.Lobby)
        {
            uiMain.ChangeUIState(WTUIState.Lobby);
        }
        else if (gameState == WTGameState.Loading)
        {
            uiMain.ChangeUIState(WTUIState.Loading);
        }
        else if (gameState == WTGameState.SelectCharacter)
        {
            //if (savedData == null) 나중에,, 스토리 패널 만들고나서,,
            //{
            //
            //    uiMain.ChangeUIState(WTUIState.Story);
            //                uiMain.ChangeUIState(WTUIState.Game);
            //WTStageTimeData data = GetCurrentStageData();
            //StartDayTimer(data);
            //}
            //else
            //{

            //처음부터 !
            uiMain.ChangeUIState(WTUIState.SelectCharacter);

            //}
            //PrepareGameStart();
        }
        else if (gameState == WTGameState.Game)
        {
            uiMain.ChangeUIState(WTUIState.Game);
            StartDayTimer(GetCurrentStageData());
        }
        else if(gameState == WTGameState.End)
        {
            uiMain.ChangeUIState(WTUIState.GameOver);
        }
            return true;
    }

    public void RestartGame()
    {
        WTUIMain uiMain = WTUIMain.Instance;
        uiMain.DestroyPanel(WTUIState.Game);
       // player.Setup();
        uiMain.ChangeUIState(WTUIState.SelectCharacter);
    }

}
