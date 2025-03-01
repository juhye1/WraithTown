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

    public void Awake()
    {
        _instance = this;
        InitDatas();

    }

    private void Start()
    {
        ChangeGameState(WTGameState.Lobby);
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
        else if (gameState == WTGameState.Game)
        {
            if (savedData == null)
            {
                uiMain.ChangeUIState(WTUIState.Story);
            }
            else
            {
                playerData = new WTGameData();
                playerData.day = 1;
                playerData.stageID = WTConstants.StartStageID;
                uiMain.ChangeUIState(WTUIState.Game);
                WTStageTimeData data = GetCurrentStageData();
                StartDayTimer(data);
            }
            //PrepareGameStart();
        }

        return true;
    }

}
