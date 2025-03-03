using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel_Shop : MonoBehaviour
{
    public TextMeshProUGUI dayTMP;
    public TextMeshProUGUI nextdayTMP;
    public TextMeshProUGUI changePanelTMP;
    public Slot_Card[] slotCards = new Slot_Card[3];
    public Sprite[] cardSprites;

    [Header("GameObject")]
    public GameObject panelCard;
    public GameObject panelUnit;
    private bool isCard = true;
    private const string strCard = "혼령\n상점";
    private const string strUnit = "유닛\n배치";
    private const string strContinue = "로 이동";
    private const string strDayEnd = " 종료!";
    private const int shuffleGold = -2;

    private void OnEnable()
    {
        WTMain main = WTMain.Instance;
        WTStageTimeData data = main.GetCurrentStageData();
        int stage = data.stage_id - 10000;
        string day = stage.ToString();
        dayTMP.SetText(WTConstants.StrDay + day + strDayEnd);
        nextdayTMP.SetText(WTConstants.StrDay + (stage+1).ToString() + strContinue);
        GetGold();
        GetRandomCards();
    }

    private void GetGold()
    {
        WTMain main = WTMain.Instance;
        SupportUnitCount unit = main.GetActiveSupportUnit(12005); // 금
        if (unit != null)
        {
            int gold = main.playerData.gold + (int)(main.playerData.gold * (unit.unitCount * 5 * 0.01));
            main.playerData.gold = gold;
        }
        else
        {

        }
    }
    private void GetRandomCards()
    {
        WTMain main = WTMain.Instance;
        WTCardData[] datas = main.cardDatas;
        Utils.Shuffle(datas);
        for(int i=0; i<slotCards.Length; ++i)
        {
            Sprite s = cardSprites[datas[i].card_color];
            slotCards[i].SetCardData(datas[i], s);
        }
    }

    public void OnClick_ShuffleBtn()
    {
        WTGlobal.CallEventDelegate(WTEventType.ChangeGold, shuffleGold);
        GetRandomCards();
        //2원 깎이게
    }

    public void OnClick_ChangePanel()
    {
        isCard = !isCard;
        string str = isCard ? strUnit : strCard;
        //changePanelTMP.SetText(str);
        panelCard.SetActive(isCard);
        panelUnit.SetActive(!isCard);
        //카드 <> 유닛
    }

    public void OnClick_ContinueGame()
    {
        WTUIMain uiMain = WTUIMain.Instance;
        WTMain main = WTMain.Instance;
        main.playerData.stageID++;
        WTGlobal.CallEventDelegate(WTEventType.ChangeStage, main.playerData.stageID);
        uiMain.ChangeUIState(WTUIState.Game);
        main.player.isPlaying = true;
        main.spawner.gameObject.SetActive(true);
        uiMain.DestroyPanel(WTUIState.Shop);
    }

}
