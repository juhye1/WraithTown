using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Game : MonoBehaviour
{
    [Header("TMP")]
    public TextMeshProUGUI dayTMP;
    public TextMeshProUGUI pointTMP;
    public TextMeshProUGUI goldTMP;
    
    [Header("Slider")]
    public Slider hpSlider;
    //public Slider daySlider;

    public RectTransform dayArrowRect;
    public RectTransform dayArrowEndRect;

    private void Awake()
    {
        ResetSlider();
    }
    private void OnEnable()
    {
        WTGlobal.RegisterEventDelegate(WTEventType.PlayerHPControl, ControlHPSlider);
        WTGlobal.RegisterEventDelegate(WTEventType.Timer, ControlDayArrow);
    }
    private void OnDisable()
    {
        WTGlobal.UnregisterEventDelegate(WTEventType.PlayerHPControl, ControlHPSlider);
        WTGlobal.UnregisterEventDelegate(WTEventType.Timer, ControlDayArrow);
    }
    public void ControlHPSlider(int hp)
    {
        hpSlider.value = hp;
    }

    public void ControlDayArrow(int time)
    {
        dayArrowRect.DOLocalMoveX(dayArrowEndRect.localPosition.x, WTConstants.TotalStageTime);
       // daySlider.value = time;
    }
    private void ResetSlider()
    {
        //daySlider.value = 0;
        hpSlider.value = WTConstants.MaxHP;
        //daySlider.maxValue = WTConstants.TotalStageTime;
        hpSlider.maxValue = WTConstants.MaxHP;
    }

}
