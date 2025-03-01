using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Card : MonoBehaviour
{
    public TextMeshProUGUI descTMP;
    public TextMeshProUGUI pointTMP;
    public Image disableImage;
    public Button btn;
    private WTCardData cardData;
    public void SetCardData(WTCardData data)
    {
        disableImage.enabled = false;
        btn.interactable = true;
        cardData = data;
        descTMP.SetText(data.card_des);
        pointTMP.SetText(data.card_coin_cost.ToString());
    }
    public void OnClickBtn_SelectCard()
    {
        disableImage.enabled = true;
        btn.interactable = false;
        WTMain main = WTMain.Instance;
        main.ChangePlayerStats((WTEffectType)cardData.effectID, cardData.value);
        WTGlobal.CallEventDelegate(WTEventType.ChangeGold, cardData.card_coin_cost * -1);
    }
}
