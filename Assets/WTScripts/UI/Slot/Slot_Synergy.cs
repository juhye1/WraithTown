using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Synergy : MonoBehaviour
{
    public Image synergyImg;
    public TextMeshProUGUI number;
    public TextMeshProUGUI synergyName;
    public TextMeshProUGUI synergyState;
    private int num = 0;
    private WTTotalSynergyDataTemplate temp;

    private void OnEnable()
    {
    }

    public void SetData(Sprite sp, WTTotalSynergyDataTemplate synergy, int count)
    {
        temp = synergy;
        Debug.Log(synergy.Total_Synergy_Name+ num);
        synergyImg.sprite = sp;
        number.SetText(count.ToString());
        synergyName.SetText(synergy.Total_Synergy_Name);
        synergyState.SetText(synergy.ThresHold_UI);
    }

    public void ShowInfo()
    {
        WTUIMain uiMain = WTUIMain.Instance;
        uiMain.panel_Game.currentSlot = this; // 임시
        WTGlobal.CallEventDelegate(WTEventType.ShowSynergyInfo, temp.Total_Synergy_ID);
    }

    public void OffInfo()
    {
        WTGlobal.CallEventDelegate(WTEventType.ShowSynergyInfo, 0);
    }
}
