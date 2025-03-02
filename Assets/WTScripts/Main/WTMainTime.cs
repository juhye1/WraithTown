using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class WTMain : MonoBehaviour
{
    [NonSerialized] public float playerTimer;
    [NonSerialized] public float nightTime;
    [NonSerialized] public bool isTimerStarted;

    public int shopTime = 300;

    public void StartDayTimer(WTStageTimeData data)
    {
        playerTimer = 0;
        nightTime = WTConstants.TotalStageTime * (data.stage_night_time * 0.01f);
        //GameObject go = Resources.Load<GameObject>("Spawner");
        //Instantiate(go);
        SpawnSpawner();
        GetPlayerAb();
        if (isTestMode)
        {
           playerTimer = WTConstants.TotalStageTime - shopTime; // 상점 테스트용
        }
        isTimerStarted = true;
        WTGlobal.CallEventDelegate(WTEventType.Timer, 3); // 테스트
    }

    private void GetPlayerAb()
    {
        //trait ab
        SupportUnitCount c = GetActiveSupportUnit(12001); //흙
        if (c != null)
        {
            playerData.playerAb.hpRecoveryPerSec = c.unitCount * 2;
        }
        //support unit ab
        c = GetActiveSupportUnit(12002); //흙
        if (c != null)
        {
            WTSupportUnitAbilityTemplate temp = GetSupportUnitAbilityTemplate(12002);
            //돌격
            playerData.playerAb.moveSpeed = playerData.playerAb.moveSpeed + (playerData.playerAb.moveSpeed * temp.support_ability_stat);
        }
        c = GetActiveSupportUnit(12003); //흙
        if (c != null)
        {
            //질투쟁이, 피흡
            WTSupportUnitAbilityTemplate temp = GetSupportUnitAbilityTemplate(12003);
            playerData.playerAb.stealEnemyHp = temp.support_ability_stat;
        }
        c = GetActiveSupportUnit(12004);
        if (c != null)
        {
            //꺼지지 않는 불, 크리
            WTSupportUnitAbilityTemplate temp = GetSupportUnitAbilityTemplate(12004);
            playerData.playerAb.doubleDamageChance = temp.support_ability_stat;
        }
        c = GetActiveSupportUnit(12005); 
        if (c != null)
        {
            //강인함
            WTSupportUnitAbilityTemplate temp = GetSupportUnitAbilityTemplate(12005);
            playerData.playerAb.enemyHalfDamageChance = c.unitCount * temp.support_ability_stat;
        }
        c = GetActiveSupportUnit(12006);
        if (c != null)
        {
            //흉내쟁이
        }
        c = GetActiveSupportUnit(12007); //흙
        if (c != null)
        {
            //가사 도우미
            WTSupportUnitAbilityTemplate temp = GetSupportUnitAbilityTemplate(12007);
            playerData.playerAb.attackRange = playerData.playerAb.attackRange + (temp.support_ability_stat);
        }
        //부자가 될거야
    }

    private void UpdateTimer(float dt)
    {
        if(isTimerStarted)
        {
            playerTimer += dt;

            if(playerData.playerAb.hpRecoveryPerSec != 0)
            {
                ChangePlayerStats(WTEffectType.HealHp, playerData.playerAb.hpRecoveryPerSec);
            }


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
