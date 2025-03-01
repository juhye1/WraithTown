using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel_DayTimer : MonoBehaviour
{
    public TextMeshProUGUI dayTMP;
    public RectTransform dayArrowRect;
    public RectTransform dayArrowEndRect;
    private float beginPosX;


    private void Start()
    {
        beginPosX = dayArrowRect.localPosition.x;
    }

    private void OnEnable()
    {
        WTGlobal.RegisterEventDelegate(WTEventType.Timer, ControlDayArrow);
        WTGlobal.RegisterEventDelegate(WTEventType.ChangeStage, ChangeDayTMP);
    }
    private void OnDisable()
    {
        WTGlobal.UnregisterEventDelegate(WTEventType.Timer, ControlDayArrow);
        WTGlobal.UnregisterEventDelegate(WTEventType.ChangeStage, ChangeDayTMP);
    }
    public void ControlDayArrow(int time)
    {
        dayArrowRect.DOLocalMoveX(dayArrowEndRect.localPosition.x, 3);
        //dayArrowRect.DOLocalMoveX(dayArrowEndRect.localPosition.x, WTConstants.TotalStageTime);
        // daySlider.value = time;
    }

    public void ChangeDayTMP(int val)
    {
        WTMain main = WTMain.Instance;
        WTStageTimeData data = main.GetCurrentStageData();
        string day = (data.stage_id - 10000).ToString();
        dayArrowRect.localPosition = new Vector3(beginPosX, dayArrowRect.localPosition.y, 0);
        dayTMP.SetText(WTConstants.StrDay + day);
        main.StartDayTimer(data);
        ControlDayArrow(3);
    }
}
