using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WTGameData
{
    public int gold, point = 0;
    public int hp = WTConstants.MaxHP;
    public ushort[] items = null;
    public ushort stageID = 0;
    public ushort day = 0;
    public ushort remainTimes = 0;
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
}

public class WTWraithStats
{
    public int userUnitId;       
    public float moveSpeed;       
    public int hp;                
    public int dmg;             
    public float attackRange;    
    public float attackSpeed;    
    public int totalTileCount;   
    public int specialTileCount; 
    public int normalTileCount; 
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


    public void SaveData()
    {
        WTGameData data = new WTGameData();
        string filePath = persistentPath + "/data.json";
        string dataToJason = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(filePath, dataToJason);
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