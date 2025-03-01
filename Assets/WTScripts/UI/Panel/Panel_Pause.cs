using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Pause : MonoBehaviour
{
    public void OnClickBtn_Save()
    {
        WTMain main = WTMain.Instance;
        main.SaveData();
    }
}
