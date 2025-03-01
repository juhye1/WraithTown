using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class WTGameData
{
    public ushort gold, point = 0;
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
    [NonSerialized] public Dictionary<ushort, WTStageTimeData> dicStageData = new();

    public void InitDatas()
    {
        dataPath = Application.dataPath;
        streamingAssetPath = Application.streamingAssetsPath;
        persistentPath = Application.persistentDataPath;
        tempPath = Application.temporaryCachePath;
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
        string jsonString = System.IO.File.ReadAllText(filePath);
        if (!string.IsNullOrEmpty(jsonString))
        {
            savedData = JsonUtility.FromJson<WTGameData>(jsonString);
            WTGlobal.CallEventDelegate(WTEventType.SaveDataLoaded, 1);

            //savedData =  
        }
        else
        {
            //�̾��ϱ� ��ư ��Ȱ��ȭ
            WTGlobal.CallEventDelegate(WTEventType.SaveDataLoaded, 0);
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