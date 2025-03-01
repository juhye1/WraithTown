using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class WTMain : MonoBehaviour
{
    [NonSerialized] public float playerTimer;
    [NonSerialized] public float nightTime;
    [NonSerialized] public bool isTimerStarted;

    public void StartDayTimer(WTStageTimeData data)
    {
        playerTimer = 0;
        nightTime = WTConstants.TotalStageTime * (data.stage_night_time * 0.01f);
        playerTimer = WTConstants.TotalStageTime -3; // 상점 테스트용
        isTimerStarted = true;
        WTGlobal.CallEventDelegate(WTEventType.Timer, 3); // 테스트
    }

    private void UpdateTimer(float dt)
    {
        if(isTimerStarted)
        {
            playerTimer += dt;
            if (playerTimer > nightTime)
            {
                // 낮으로 전환
                if (playerTimer > WTConstants.TotalStageTime)
                {
                    WTUIMain uiMain = WTUIMain.Instance;
                    uiMain.ChangeUIState(WTUIState.Shop);
                    //스테이지 끝
                    isTimerStarted = false;
                }
            }
        }
    }
}
