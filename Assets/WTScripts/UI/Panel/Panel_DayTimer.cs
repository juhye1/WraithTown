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

    private void OnEnable()
    {
        WTGlobal.RegisterEventDelegate(WTEventType.Timer, ControlDayArrow);
    }
    private void OnDisable()
    {
        WTGlobal.UnregisterEventDelegate(WTEventType.Timer, ControlDayArrow);
    }
    public void ControlDayArrow(int time)
    {
        dayArrowRect.DOLocalMoveX(dayArrowEndRect.localPosition.x, WTConstants.TotalStageTime);
        // daySlider.value = time;
    }
}
