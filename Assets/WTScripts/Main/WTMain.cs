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

    public void Awake()
    {
        _instance = this;
        InitDatas();
    }

}
