using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SupportUnitCount
{
    public ushort unitID;
    public int unitCount;
    public SupportUnitCount(ushort id, int count)
    {
        unitID = id;
        unitCount = count;
    }
}

[Serializable]
public class WTGameData
{
    public int userUnitId = 0;
    public int gold, point = 0;
    public int currentHP = 0;
    public ushort[] items = null;
    public ushort stageID = 0;
    public ushort day = 0;
    public ushort remainTimes = 0;
    public ushort[] speicalTiles = null;
    public WTPlayerAbility playerAb;
    public List<SupportUnitCount> supportUnits = new();
    public List<ushort> totalSupportUnits = new();
    public List<SupportUnitCount> activeSupportUnits = new();
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
    public byte card_color;
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
    public int special_tile_count;

    public WTPlayerAbility(WTWraithStatTemplate temp)
    {
        maxHP = temp.hp;
        hpRecoveryPerSec = 0;
        damage = temp.dmg;
        moveSpeed = temp.move_speed;
        attackRange = temp.attack_range;
        projectileCount = 1;
        special_tile_count = temp.special_tile_count;
    }
}

[Serializable]
public class WTEnemyUnitStatsTemplate
{
    public ushort enemyunit_id;
    public string enemy_name;
    public float spawn_start_time;
    public float spawn_end_time;
    public float spawn_weight;
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
    public float support_ability_stat;
}
[Serializable]
public class WTSupportUnitAbilityTemplateGroup
{
    public WTSupportUnitAbilityTemplate[] supportUnitAbilityDatas;
}

[Serializable]
public class WTSupportUnitTemplate
{
    public ushort support_unit_id;
    public string support_unit_name;
    public ushort synergy_id;
    public ushort trait_id;
}
[Serializable]
public class WTSupportUnitTemplateGroup
{
    public WTSupportUnitTemplate[] supportUnitDatas;
}

[Serializable]
public class WTSynergyTemplate
{
    public ushort synergy_id;        // 시너지 ID
    public ushort synergy_type;
    public string synergy_name;   // 시너지 이름
    public int threshold;         // 효과 발동 조건 (예: 2개 이상)
    public string effect_des;     // 효과 설명
   [NonSerialized] public string synergy_threshold;   // 시너지 이름
}

[Serializable]
public class WTSynergyTemplateGroup
{
    public WTSynergyTemplate[] synergyDatas;
}

[Serializable]
public class WTWraithIDTemplate
{
    public ushort userunit_id;     // 유닛 ID
    public string wraith_name;  // 망령 이름
    public int synergy_id;      // 시너지 ID
    public int trait_id;        // 특성 ID
    [NonSerialized] public string trait_threshold;   // 시너지 이름
}
[Serializable]
public class WTWraithIDTemplateGroup
{
    public WTWraithIDTemplate[] wraithIdDatas;
}

[Serializable]
public class WTTraitDataTemplate
{
    public ushort trait_id;        // 특성 ID
    public string trait_name;   // 특성 이름
    public int threshold;       // 효과 발동 조건
    public string effect_des;   // 효과 설명
}
[Serializable]
public class WTTraitDataTemplateGroup
{
    public WTTraitDataTemplate[] traitDatas;
}

[Serializable]
public class WTAttributeTileTemplate
{
    public ushort tile_attribute_id;       // 타일 속성 ID
    public ushort support_unit_id;         // 지원 유닛 ID
    public string special_tile_value_text; // 특수 타일 값 (문자열)
    public int special_tile_value;      // 특수 타일 값 (숫자)
}
[Serializable]
public class WTAttributeTileTemplateGroup
{
    public WTAttributeTileTemplate[] attributeTileDatas;
}

[Serializable]
public class WTTotalSynergyDataTemplate
{
    public ushort Total_Synergy_ID;         // 전체 시너지 ID
    public string Total_Synergy_Name;    // 전체 시너지 이름
    public string Card_Effect_Name;      // 카드 효과 이름
    public int[] Effect_Value;           // 효과 값 (배열로 변환)
    public string ThresHold_UI;          // 조건 UI 표시
}

[Serializable]
public class WTTotalSynergyDataTemplateGroup
{
    public WTTotalSynergyDataTemplate[] totalSynergyDatas;
}


[Serializable]
public class WTStageTimeData
{
    public ushort stage_id, stage_night_time, stage_day_time, total_stage_time, SpawnRate, EnemiesPerWave = 0;
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
    [NonSerialized] public WTSupportUnitTemplate[] supportUnitDatas = null;
    [NonSerialized] public ushort[] synergyIds;

    [NonSerialized] public Dictionary<ushort, WTStageTimeData> dicStageData = new();
    [NonSerialized] public Dictionary<ushort, WTWraithStatTemplate> dicPlayerStatTemplate = new();
    [NonSerialized] public Dictionary<ushort, WTSupportUnitAbilityTemplate> dicSupportUnitAbilityTemplate = new();
    [NonSerialized] public Dictionary<ushort, WTSupportUnitTemplate> dicSupportUnitTemplate = new();
    [NonSerialized] public Dictionary<ushort, WTEnemyUnitStatsTemplate> dicEnemyUnitStatsTemplate = new();
    [NonSerialized] public Dictionary<ushort, WTSynergyTemplate> dicSynergyTemplate = new();
    [NonSerialized] public Dictionary<ushort, WTWraithIDTemplate> dicWraithIDTemplate= new();
    [NonSerialized] public Dictionary<ushort, WTTraitDataTemplate> dicTraitDataTemplate= new();
    [NonSerialized] public Dictionary<ushort, WTAttributeTileTemplate> dicTileDataTemplate= new();
    [NonSerialized] public Dictionary<ushort, WTTotalSynergyDataTemplate> dicTotalSynergyTemplate= new();
    [NonSerialized] public Dictionary<ushort, Sprite[]> dicSynergeSprites= new();

    public void InitDatas()
    {
        dataPath = Application.dataPath;
        streamingAssetPath = Application.streamingAssetsPath;
        persistentPath = Application.persistentDataPath;
        tempPath = Application.temporaryCachePath;
        synergyIds = new ushort[7] { WTConstants.SynergyIDGhost, WTConstants.SynergyIDYoukai, WTConstants.TraitIDSoil, WTConstants.TraitIDFire, WTConstants.TraitIDGold,
        WTConstants.TraitIDMoon, WTConstants.TraitIDWater};
        LoadAddressable();
        LoadTemplates();
        LoadSavedData();
        AddSpriteToDic();
        //LoadOptionFromJson();
        //LoadQuestTemplate();
        //LoadMapTemplate();
        //LoadNPCTemplate();
        //LoadPlayerCharacterTemplate();
        //LoadLocalizationTable();
        //LoadSoundTemplate();
    }

    public void LoadAddressable()
    {
        var obj = Resources.Load<WTResourcesManager>("WTResourcesManager");
        if (obj != null)
        {
            var instance = Instantiate(obj); // 씬에 인스턴스 생성
            instance.gameObject.SetActive(true);
        }
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

        //보호 유닛 어빌리티 정보
        TextAsset supportTa = Resources.Load<TextAsset>("Template/SupportUnitAbilityTemplate");
        WTSupportUnitAbilityTemplateGroup tempSpData = JsonUtility.FromJson<WTSupportUnitAbilityTemplateGroup>(supportTa.text);
        for (int i = 0; i < tempSpData.supportUnitAbilityDatas.Length; ++i)
        {
            WTSupportUnitAbilityTemplate s = tempSpData.supportUnitAbilityDatas[i];
            dicSupportUnitAbilityTemplate.Add(s.support_unit_id, s);
        }

        //보호 유닛 정보 
        TextAsset supportUnitTa = Resources.Load<TextAsset>("Template/SupportUnitTemplate");
        WTSupportUnitTemplateGroup tempSpUnitData = JsonUtility.FromJson<WTSupportUnitTemplateGroup>(supportUnitTa.text);
        supportUnitDatas = tempSpUnitData.supportUnitDatas;
        for (int i = 0; i < tempSpUnitData.supportUnitDatas.Length; ++i)
        {
            WTSupportUnitTemplate s = tempSpUnitData.supportUnitDatas[i];
            dicSupportUnitTemplate.Add(s.support_unit_id, s);
        }

        //시너지 정보
        TextAsset synergyTa = Resources.Load<TextAsset>("Template/SynergyInfoTemplate");
        WTSynergyTemplateGroup tempSynergyData = JsonUtility.FromJson<WTSynergyTemplateGroup>(synergyTa.text);
        for (int i = 0; i < tempSynergyData.synergyDatas.Length; ++i)
        {
            WTSynergyTemplate s = tempSynergyData.synergyDatas[i];
            dicSynergyTemplate.Add(s.synergy_id, s);
        }

        //캐릭터 ID 정보 묶음
        TextAsset idTa = Resources.Load<TextAsset>("Template/WraithIDTemplate");
        WTWraithIDTemplateGroup idData = JsonUtility.FromJson<WTWraithIDTemplateGroup>(idTa.text);
        for (int i = 0; i < idData.wraithIdDatas.Length; ++i)
        {
            WTWraithIDTemplate s = idData.wraithIdDatas[i];
            dicWraithIDTemplate.Add(s.userunit_id, s);
        }

        //타일
        TextAsset tileTa = Resources.Load<TextAsset>("Template/AttributeTileTemplate");
        WTAttributeTileTemplateGroup tileData = JsonUtility.FromJson<WTAttributeTileTemplateGroup>(tileTa.text);
        for (int i = 0; i < tileData.attributeTileDatas.Length; ++i)
        {
            WTAttributeTileTemplate s = tileData.attributeTileDatas[i];
            dicTileDataTemplate.Add(s.tile_attribute_id, s);
        }

        //특성 정보
        TextAsset traitInfoTa = Resources.Load<TextAsset>("Template/TraitInfoTemplate");
        WTTraitDataTemplateGroup traitInfoData = JsonUtility.FromJson<WTTraitDataTemplateGroup>(traitInfoTa.text);
        for (int i = 0; i < traitInfoData.traitDatas.Length; ++i)
        {
            WTTraitDataTemplate s = traitInfoData.traitDatas[i];
            dicTraitDataTemplate.Add(s.trait_id, s);
        }

        //적 유닛 스탯 정보
        TextAsset enemyStatsTa = Resources.Load<TextAsset>("Template/EnemyUnitStatsTemplate");
        WTEnemyUnitStatsTemplateGroup tempEnemyData = JsonUtility.FromJson<WTEnemyUnitStatsTemplateGroup>(enemyStatsTa.text);
        for (int i = 0; i < tempEnemyData.enemyUnitStats.Length; ++i)
        {
            WTEnemyUnitStatsTemplate s = tempEnemyData.enemyUnitStats[i];
            dicEnemyUnitStatsTemplate.Add(s.enemyunit_id, s);
        }

        //적 유닛 스탯 정보
        TextAsset totalSynergyTa = Resources.Load<TextAsset>("Template/TotalSynergyTemplate");
        WTTotalSynergyDataTemplateGroup totalSynergyGroup = JsonUtility.FromJson<WTTotalSynergyDataTemplateGroup>(totalSynergyTa.text);
        for (int i = 0; i < totalSynergyGroup.totalSynergyDatas.Length; ++i)
        {
            WTTotalSynergyDataTemplate s = totalSynergyGroup.totalSynergyDatas[i];
            dicTotalSynergyTemplate.Add(s.Total_Synergy_ID, s);
        }
    }

    public Sprite GetSynergySprite(ushort ID, int lv)
    {
        Sprite[] result;
        dicSynergeSprites.TryGetValue(ID, out result);
        return result[lv];
    }

    public void AddSpriteToDic()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Synerge");

        for(int i=0; i< synergyIds.Length; ++i)
        {
            dicSynergeSprites.Add(synergyIds[i], new Sprite[4]);
        }

        int count = 0;
        for (int i = 0; i < sprites.Length; ++i)
        {
            string spriteName = sprites[i].name;
            string[] parts = spriteName.Split('_'); // 언더바(_) 기준으로 분리
            if (parts.Length >= 3)
            {
                string result = parts[1]; // 가운데 단어 가져오기
                ushort ID = 0;
                switch(result)
                {
                    case "soil":
                        ID = WTConstants.TraitIDSoil;
                        break;
                    case "gold":
                        ID = WTConstants.TraitIDGold;
                        break;
                    case "moon":
                        ID = WTConstants.TraitIDMoon;
                        break;
                    case "water":
                        ID = WTConstants.TraitIDWater;
                        break;
                    case "fire":
                        ID = WTConstants.TraitIDFire;
                        break;
                    case "ghost":
                        ID = WTConstants.SynergyIDGhost;
                        break;
                    case "youkai":
                        ID = WTConstants.SynergyIDYoukai;
                        break;

                }
                dicSynergeSprites[ID][count] = sprites[i];
                count++;
                if(count > 3)
                {
                    count = 0;
                }
                Debug.Log(sprites[i].name);
            }
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

    public WTSynergyTemplate GetSynergeTemplate(ushort TID)
    {
        WTSynergyTemplate result;
        dicSynergyTemplate.TryGetValue(TID, out result);
        return result;
    }

    public WTTraitDataTemplate GetTraitDataTemplate(ushort TID)
    {
        WTTraitDataTemplate result;
        dicTraitDataTemplate.TryGetValue(TID, out result);
        return result;
    }

    public WTTotalSynergyDataTemplate GetTotalSynergeTemplate(ushort TID)
    {
        WTTotalSynergyDataTemplate result;
        dicTotalSynergyTemplate.TryGetValue(TID, out result);
        return result;
    }


    
    public WTWraithStatTemplate GetPlayerCharacterTemplate(ushort TID)
    {
        WTWraithStatTemplate result;
        dicPlayerStatTemplate.TryGetValue(TID, out result);
        return result;
    }

    public WTSupportUnitTemplate GetSupportUnitTemplate(ushort TID)
    {
        WTSupportUnitTemplate result;
        dicSupportUnitTemplate.TryGetValue(TID, out result);
        return result;
    }

    public void SaveData()
    {
        WTGameData data = new WTGameData();
        string filePath = persistentPath + "/data.json";
        string dataToJason = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(filePath, dataToJason);
    }

    public void GetSupportSynergyUnit(ushort synergyID)
    {
        Utils.Shuffle(supportUnitDatas);
        WTSupportUnitTemplate unit = null;
        for(int i=0; i<supportUnitDatas.Length; ++i)
        {
            WTSupportUnitTemplate temp = supportUnitDatas[i];
            if(temp.synergy_id == synergyID)
            {
                unit = temp;
                break;
            }
        }
        AddUnit(unit.support_unit_id);
        playerData.totalSupportUnits.Add(unit.support_unit_id);
        WTGlobal.CallEventDelegate(WTEventType.AddSynergy, unit.support_unit_id);
    }

    public void AddUnit(ushort addUnitID)
    {
        for(int i=0; i<playerData.supportUnits.Count; ++i)
        {
            ushort id = playerData.supportUnits[i].unitID;
            if(id == addUnitID)
            {
                playerData.supportUnits[i].unitCount++;
                return;
            }
        }
        SupportUnitCount c = new(addUnitID, 1);
        playerData.supportUnits.Add(c);
    }

    public void AddActiveUnit(ushort addUnitID)
    {
        for (int i = 0; i < playerData.activeSupportUnits.Count; ++i)
        {
            ushort id = playerData.activeSupportUnits[i].unitID;
            if (id == addUnitID)
            {
                playerData.activeSupportUnits[i].unitCount++;
                return;
            }
        }
        SupportUnitCount c = new(addUnitID, 1);
        playerData.activeSupportUnits.Add(c);
    }

    public void GetSupportTraitUnit(ushort traitID)
    {
        Utils.Shuffle(supportUnitDatas);
        WTSupportUnitTemplate unit = null;
        for (int i = 0; i < supportUnitDatas.Length; ++i)
        {
            WTSupportUnitTemplate temp = supportUnitDatas[i];
            if (temp.trait_id == traitID)
            {
                unit = temp;
                break;
            }
        }
        AddUnit(unit.support_unit_id);
        playerData.totalSupportUnits.Add(unit.support_unit_id);
        WTGlobal.CallEventDelegate(WTEventType.AddSynergy, unit.support_unit_id);
        Debug.Log("ADD" + unit.support_unit_name);
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
                for (int i = 0; i < player.tiles.Count; ++i)
                {
                    WraithTile tile = player.tiles[i];
                    if(!tile.isSpecial)
                    {
                        tile.SetTileSpecialColor();
                        break;
                    }
                }
                break;
            case WTEffectType.TilePositionChange:
                //타일 위치 변경
                Utils.Shuffle(player.tiles);
                for (int i = 0; i < player.tiles.Count; ++i)
                {
                    WraithTile tile = player.tiles[i];
                    if (i < playerData.playerAb.special_tile_count)
                    {
                        tile.SetTileSpecialColor();
                    }
                    else
                    {
                        tile.SetTileNormalColor();
                    }
                }

                break;


                //밑으로 유닛 랜덤 획득
            case WTEffectType.GetRandomSoilUnit:
                // Trait
                
                GetSupportTraitUnit(WTConstants.TraitIDSoil);
                break;
            case WTEffectType.GetRandomFireUnit:
                GetSupportTraitUnit(WTConstants.TraitIDFire);
                break;
            case WTEffectType.GetRandomGoldUnit:
                GetSupportTraitUnit(WTConstants.TraitIDGold);
                break;
            case WTEffectType.GetRandomMoonUnit:
                GetSupportTraitUnit(WTConstants.TraitIDMoon);
                break;
            case WTEffectType.GetRandomWaterUnit:
                GetSupportTraitUnit(WTConstants.TraitIDWater);
                //Synergy
                break;
            case WTEffectType.GetRandomGhostUnit:
                GetSupportSynergyUnit(WTConstants.SynergyIDGhost);
                break;
            case WTEffectType.GetRandomYokaiUnit:
                GetSupportSynergyUnit(WTConstants.SynergyIDYoukai);

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