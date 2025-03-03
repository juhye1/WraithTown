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
    public Image cardImage;
    public Button btn;
    private WTCardData cardData;
    public void SetCardData(WTCardData data, Sprite sp)
    {
        disableImage.enabled = false;
        btn.interactable = true;
        cardData = data;
        cardImage.sprite = sp;
        descTMP.SetText(data.card_des);
        pointTMP.SetText(data.card_coin_cost.ToString());
    }
    public void OnClickBtn_SelectCard()
    {
        WTMain main = WTMain.Instance;
        if (main.playerData.gold < cardData.card_coin_cost)
        {
            return;
        }
        disableImage.enabled = true;
        btn.interactable = false;
        main.ChangePlayerStats((WTEffectType)cardData.effectID, cardData.value);
        WTGlobal.CallEventDelegate(WTEventType.ChangeGold, cardData.card_coin_cost * -1);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bgmClip["BuyCard"]);
    }
}
