using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_SynergyInfo : MonoBehaviour
{
    public GameObject InfoGo;
    public Image synergyIconImage;
    public TextMeshProUGUI synergyNameTMP;
    public TextMeshProUGUI synergyDescTMP;
    public TextMeshProUGUI synergyTMP;
    private StringBuilder sb = new StringBuilder(100);


    [Header("Sprites")]
    public Sprite[] synergySprites;

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

        WTUIMain uiMain = WTUIMain.Instance;
        InfoGo.transform.localPosition = new Vector3(InfoGo.transform.localPosition.x   , uiMain.panel_Game.currentSlot.transform.localPosition.y, 0);


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
        WTMain main = WTMain.Instance;
        synergyNameTMP.SetText(info.synergy_name);
        synergyDescTMP.SetText(info.mouseover_des);
        synergyIconImage.sprite = synergySprites[data.spriteKey];
        ushort id = data.Total_Synergy_ID;
        for (int i = 0; i < 3; ++i)
        {
            WTSynergyTemplate traitTemp = main.GetSynergeTemplate(id);
            id++;
            sb.Append("(" + i + ")");
            sb.AppendLine(info.synergy_effect_value);
        }
        synergyTMP.SetText(sb.ToString());
        sb.Clear();
    }

    public void SetTraitData(WTTotalSynergyDataTemplate data, WTTraitDataTemplate info)
    {
        WTMain main = WTMain.Instance;

        synergyNameTMP.SetText(info.trait_name);
        synergyDescTMP.SetText(info.mouseover_des);
        synergyIconImage.sprite = synergySprites[data.spriteKey];
        ushort id = data.Total_Synergy_ID;
        for (int i = 0; i < 3; ++i)
        {
            WTTraitDataTemplate traitTemp = main.GetTraitDataTemplate(id);
            id++;
            sb.Append("(" + i + ")");
            sb.AppendLine(info.trait_effect_value);
        }
        synergyTMP.SetText(sb.ToString());
        sb.Clear();
        //WTTraitDataTemplate traitTemp = main.GetTraitDataTemplate(id);
    }

}
