using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel_Shop : MonoBehaviour
{
    public TextMeshProUGUI dayTMP;
    public TextMeshProUGUI changePanelTMP;
    public Slot_Card[] slotCards = new Slot_Card[3];

    [Header("GameObject")]
    public GameObject panelCard;
    public GameObject panelUnit;
    private bool isCard = true;

    private void OnEnable()
    {
        GetRandomCards();
    }
    private void GetRandomCards()
    {
        WTMain main = WTMain.Instance;
        WTCardData[] datas = main.cardDatas;
        Utils.Shuffle(datas);
        for(int i=0; i<slotCards.Length; ++i)
        {
            slotCards[i].SetCardData(datas[i]);
        }
    }

    public void OnClick_ShuffleBtn()
    {
        WTGlobal.CallEventDelegate(WTEventType.ChangeGold, -2);
        GetRandomCards();
        //2원 깎이게
    }

    public void OnClick_ChangePanel()
    {
        isCard = !isCard;
        panelCard.SetActive(isCard);
        panelCard.SetActive(!isCard);
        //2원 깎이게
    }

}
