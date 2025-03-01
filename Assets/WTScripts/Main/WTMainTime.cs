using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class WTMain : MonoBehaviour
{
    public float playerTimer;
    public float nightTime;
    public bool isTimerStarted;

    public void StartDayTimer(WTStageTimeData data)
    {
        playerTimer = 0;
        nightTime = WTConstants.TotalStageTime * (data.stage_night_time * 0.01f);
        isTimerStarted = true;
    }

    private void UpdateTimer(float dt)
    {
        if(isTimerStarted)
        {
            playerTimer += dt;
            WTGlobal.CallEventDelegate(WTEventType.Timer, (int)playerTimer);
            if(playerTimer > nightTime) 
            {
                if(playerTimer > WTConstants.TotalStageTime)
                {
                    isTimerStarted = false;
                }
            }
        }
    }
}
