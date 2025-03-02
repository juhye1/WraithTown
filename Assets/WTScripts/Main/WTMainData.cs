using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WTGameData
{
    public int gold, point = 0;
    public int currentHP = 0;
    public ushort[] items = null;
    public ushort stageID = 0;
    public ushort day = 0;
    public ushort remainTimes = 0;
    public WTPlayerAbility playerAb;
    public List<ushort> supportUnits = new();
}
[Serializable]
public class WTStageData
{
    public WTStageTimeData[] timeDatas;
}

[Serializable]
public class WTCardDataTemplate
{
    public WTCardData[] cardDatas;
}

[Serializable]
public class WTCardData
{
    public ushort card_Id;   
    public string card_name;  
    public ushort value;  
    public ushort card_coin_cost;
    public ushort spawn_weight;
    public string card_des;
    public string EffectType;
    public byte effectID;
}
[Serializable]
public class WTWraithStatTemplate
{
    public ushort userunit_id;   
    public float move_speed;   
    public int hp;          
    public int dmg;              
    public float attack_range;    
    public float attack_speed;
    public int total_tile_count;
    public int special_tile_count;
    public int normal_tile_count;

    public string basePrefabName = "Player";
    [NonSerialized] public GameObject basePrefab;
}

[Serializable]
public class WTPlayerAbility
{
    public int maxHP;
    public int hpRecoveryPerSec;
    public int damage;
    public float moveSpeed;
    public float attackRange;
    public float attackSpeed;
    public int projectileCount;

    public WTPlayerAbility(WTWraithStatTemplate temp)
    {
        maxHP = temp.hp;
        hpRecoveryPerSec = 0;
        damage = temp.dmg;
        moveSpeed = temp.move_speed;
        attackRange = temp.attack_range;
        projectileCount = 1;
    }
}

[Serializable]
public class WTEnemyUnitStatsTemplate
{
    public ushort enemyunit_id;
    public string enemy_name;
    public float spawn_start_time;
    public float spawn_end_time;
    public int spawn_weight;
    public float move_speed;
    public int hp;
    public int dmg;
    public float attack_range;
    public float attack_speed;
    public int drop_weight;
    public int dead_drop_soul;
    public int dead_drop_coin;
}
[Serializable]
public class WTEnemyUnitStatsTemplateGroup
{
    public WTEnemyUnitStatsTemplate[] enemyUnitStats;
}


[Serializable]
public class WTWraithStatTemplateGroup
{
    public WTWraithStatTemplate[] playerStatDatas;
}

[Serializable]
public class WTSupportUnitAbilityTemplate
{
    public ushort support_unit_id;
    public string support_ability_name; 
    public string support_ability; 
}
[Serializable]
public class WTSupportUnitAbilityTemplateGroup
{
    public WTSupportUnitAbilityTemplate[] supportUnitDatas;
}

[Serializable]
public class WTStageTimeData
{
    public ushort stage_id, stage_night_time, stage_day_time, total_stage_time = 0;
}

public partial class WTMain : MonoBehaviour
{
    [NonSerialized] public string dataPath;
    [NonSerialized] public string streamingAssetPath;
    [NonSerialized] public string tempPath;
    [NonSerialized] public string persistentPath;
    [NonSerialized] public WTGameData savedData = null;
    [NonSerialized] public WTGameData playerData = null;
    [NonSerialized] public WTCardData[] cardDatas = null;
    [NonSerialized] public Dictionary<ushort, WTStageTimeData> dicStageData = new();
    [NonSerialized] public Dictionary<ushort, WTWraithStatTemplate> dicPlayerStatTemplate = new();
    [NonSerialized] public Dictionary<ushort, WTSupportUnitAbilityTemplate> dicSupportUnitAbilityTemplate = new();
    [NonSerialized] public Dictionary<ushort, WTEnemyUnitStatsTemplate> dicEnemyUnitStatsTemplate = new();

    public void InitDatas()
    {
        dataPath = Application.dataPath;
        streamingAssetPath = Application.streamingAssetsPath;
        persistentPath = Application.persistentDataPath;
        tempPath = Application.temporaryCachePath;
        LoadTemplates();
        LoadSavedData();
        //LoadOptionFromJson();
        //LoadQuestTemplate();
        //LoadMapTemplate();
        //LoadNPCTemplate();
        //LoadPlayerCharacterTemplate();
        //LoadLocalizationTable();
        //LoadSoundTemplate();
    }
    public void LoadSavedData()
    {
        string filePath = persistentPath + "/data.json";
        if (System.IO.File.Exists(filePath))
        {
            string jsonString = System.IO.File.ReadAllText(filePath);
            savedData = JsonUtility.FromJson<WTGameData>(jsonString);
            WTGlobal.CallEventDelegate(WTEventType.SaveDataLoaded, 1);
        }
        else
        {
            WTGlobal.CallEventDelegate(WTEventType.SaveDataLoaded, 0);
        }
    }
    public void LoadTemplates()
    {
        //스테이지 시간 정보
        TextAsset ta = Resources.Load<TextAsset>("Template/StageTimeTemplate");
        WTStageData tempData = JsonUtility.FromJson<WTStageData>(ta.text);
        for (int i = 0; i < tempData.timeDatas.Length; ++i)
        {
            WTStageTimeData data = tempData.timeDatas[i];
            dicStageData.Add(data.stage_id, data);
        }

        //상점 카드
        TextAsset cardTa = Resources.Load<TextAsset>("Template/ShopCardTemplate");
        WTCardDataTemplate tempCardData = JsonUtility.FromJson<WTCardDataTemplate>(cardTa.text);
        cardDatas = new WTCardData[tempCardData.cardDatas.Length];
        for (int i = 0; i < tempCardData.cardDatas.Length; ++i)
        {
            cardDatas[i] = tempCardData.cardDatas[i];
            WTEffectType effectType = (WTEffectType)Enum.Parse(typeof(WTEffectType), tempCardData.cardDatas[i].EffectType);
            cardDatas[i].effectID = (byte)effectType;
        }

        //유저 정보
        TextAsset userTa = Resources.Load<TextAsset>("Template/WraithStatsTemplate");
        WTWraithStatTemplateGroup tempUserData = JsonUtility.FromJson<WTWraithStatTemplateGroup>(userTa.text);
        for (int i = 0; i < tempUserData.playerStatDatas.Length; ++i)
        {
            WTWraithStatTemplate s = tempUserData.playerStatDatas[i];
            dicPlayerStatTemplate.Add(s.userunit_id, s);
        }

        //보호 유닛 활성화 정보
        TextAsset supportTa = Resources.Load<TextAsset>("Template/SupportUnitAbilityTemplate");
        WTSupportUnitAbilityTemplateGroup tempSpData = JsonUtility.FromJson<WTSupportUnitAbilityTemplateGroup>(supportTa.text);
        for (int i = 0; i < tempSpData.supportUnitDatas.Length; ++i)
        {
            WTSupportUnitAbilityTemplate s = tempSpData.supportUnitDatas[i];
            dicSupportUnitAbilityTemplate.Add(s.support_unit_id, s);
        }
        //적 유닛 스탯 정보
        TextAsset enemyStatsTa = Resources.Load<TextAsset>("Template/EnemyUnitStatsTemplate");
        WTEnemyUnitStatsTemplateGroup tempEnemyData = JsonUtility.FromJson<WTEnemyUnitStatsTemplateGroup>(enemyStatsTa.text);
        for (int i = 0; i < tempEnemyData.enemyUnitStats.Length; ++i)
        {
            WTEnemyUnitStatsTemplate s = tempEnemyData.enemyUnitStats[i];
            dicEnemyUnitStatsTemplate.Add(s.enemyunit_id, s);
        }
    }

    public WTStageTimeData GetCurrentStageData()
    {
        WTStageTimeData timeData = null;
        ushort stageID = playerData.stageID;
        if (dicStageData.TryGetValue(playerData.stageID, out timeData))
        {
            return timeData;
        }
        return null;
    }

    public WTSupportUnitAbilityTemplate GetSupportUnitAbilityTemplate(ushort TID)
    {
        WTSupportUnitAbilityTemplate result;
        dicSupportUnitAbilityTemplate.TryGetValue(TID, out result);
        return result;
    }

    

    public WTWraithStatTemplate GetPlayerCharacterTemplate(ushort TID)
    {
        WTWraithStatTemplate result;
        dicPlayerStatTemplate.TryGetValue(TID, out result);
        return result;
    }


    public void SaveData()
    {
        WTGameData data = new WTGameData();
        string filePath = persistentPath + "/data.json";
        string dataToJason = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(filePath, dataToJason);
    }

    public void ChangePlayerStats(WTEffectType type, int val)
    {
        WTPlayerAbility ab = playerData.playerAb;
        switch (type)
        {
            case WTEffectType.MaxHPIncrease:
                ab.maxHP += val;
                WTGlobal.CallEventDelegate(WTEventType.PlayerMaxHPControl, ab.maxHP);
                break;
            case WTEffectType.HealHp:
                int healHP = playerData.currentHP + val;
                if(healHP > ab.maxHP)
                {
                    playerData.currentHP = ab.maxHP;
                }
                else
                {
                    playerData.currentHP += val;
                }
                WTGlobal.CallEventDelegate(WTEventType.PlayerHPControl, playerData.currentHP);
                break;
            case WTEffectType.RangeIncrease:
                ab.attackRange += val;
                break;
            case WTEffectType.AttackSpeedIncrease:
                ab.attackSpeed += val;
                break;
            case WTEffectType.ProjectileCountIncrease:
                ab.projectileCount += val;
                break;
            case WTEffectType.TileGradeChange:
                //타일 등급 변경
                break;
            case WTEffectType.TilePositionChange:
                //타일 위치 변경
                break;
                //밑으로 유닛 랜덤 획득
            case WTEffectType.GetRandomSoilUnit:
                break;
            case WTEffectType.GetRandomFireUnit:
                break;
            case WTEffectType.GetRandomGoldUnit:
                break;
            case WTEffectType.GetRandomMoonUnit:
                break;
            case WTEffectType.GetRandomWaterUnit:
                break;
            case WTEffectType.GetRandomGhostUnit:
                break;
            case WTEffectType.GetRandomYokaiUnit:
                break;
            case WTEffectType.Count:
                break;
        }
    }
}

public enum WTPopUpMsgType
{
    OptionJsonNotExist,

    UI_Retry,
    UI_Reject,
    UI_Accept,
    UI_Yes,
    UI_No,

    Count
}