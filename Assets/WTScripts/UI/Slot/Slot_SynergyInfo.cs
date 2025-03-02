using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Slot_SynergyInfo : MonoBehaviour
{
    public GameObject InfoGo;
    public TextMeshProUGUI synergyNameTMP;
    public TextMeshProUGUI synergyDescTMP;
    public TextMeshProUGUI synergyTMP;

    private void OnEnable()
    {
        WTGlobal.RegisterEventDelegate(WTEventType.ShowSynergyInfo, ShowInfo);
    }

    private void OnDisable()
    {
        WTGlobal.UnregisterEventDelegate(WTEventType.ShowSynergyInfo, ShowInfo);
    }

    private void ShowInfo(int val)
    {
        if(val == 0)
        {
            InfoGo.SetActive(false);
            return;
        }
        InfoGo.SetActive(true);
        WTMain main = WTMain.Instance;
        WTTotalSynergyDataTemplate temp = main.GetTotalSynergeTemplate((ushort)val);
        if(temp.Total_Synergy_ID > 10199)
        {
            WTTraitDataTemplate traitTemp = main.GetTraitDataTemplate(temp.Total_Synergy_ID);
            SetTraitData(temp, traitTemp);
            //trait
        }
        else
        {
            WTSynergyTemplate synergyTemp = main.GetSynergeTemplate(temp.Total_Synergy_ID);
            SetSynergyData(temp, synergyTemp);
            //synergy
        }
    }

    public void SetSynergyData(WTTotalSynergyDataTemplate data, WTSynergyTemplate info)
    {
        synergyNameTMP.SetText(info.synergy_name);
    }

    public void SetTraitData(WTTotalSynergyDataTemplate data, WTTraitDataTemplate info)
    {
        synergyNameTMP.SetText(info.trait_name);
    }

}
