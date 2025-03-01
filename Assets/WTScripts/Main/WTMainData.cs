using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class WTMain : MonoBehaviour
{
    [NonSerialized] public string dataPath;
    [NonSerialized] public string streamingAssetPath;
    [NonSerialized] public string tempPath;

    public void InitDatas()
    {
        dataPath = Application.dataPath;
        streamingAssetPath = Application.streamingAssetsPath;
        tempPath = Application.temporaryCachePath;

        //LoadOptionFromJson();
        //LoadQuestTemplate();
        //LoadMapTemplate();
        //LoadNPCTemplate();
        //LoadPlayerCharacterTemplate();
        //LoadLocalizationTable();
        //LoadSoundTemplate();
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