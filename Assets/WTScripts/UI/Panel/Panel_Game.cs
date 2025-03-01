using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Game : MonoBehaviour
{
    public RectTransform RT;
    [Header("TMP")]
    public TextMeshProUGUI pointTMP;
    public TextMeshProUGUI goldTMP;
    public TextMeshProUGUI hpTMP;
    
    [Header("Slider")]
    public Slider hpSlider;
    //public Slider daySlider;

    private void Awake()
    {
        RT = transform as RectTransform;
        ResetSlider();
        WTMain main = WTMain.Instance;
        ControlGold(100);
        ControlPoint(100);
    }
    private void OnEnable()
    {
        WTGlobal.RegisterEventDelegate(WTEventType.PlayerHPControl, ControlHPSlider);
        WTGlobal.RegisterEventDelegate(WTEventType.PlayerMaxHPControl, ControlMaxHPSlider);
        WTGlobal.RegisterEventDelegate(WTEventType.ChangeGold, ControlGold);
        WTGlobal.RegisterEventDelegate(WTEventType.ChangePoint, ControlPoint);
    }
    private void OnDisable()
    {
        WTGlobal.UnregisterEventDelegate(WTEventType.PlayerHPControl, ControlHPSlider);
        WTGlobal.UnregisterEventDelegate(WTEventType.PlayerMaxHPControl, ControlMaxHPSlider);
        WTGlobal.UnregisterEventDelegate(WTEventType.ChangeGold, ControlGold);
        WTGlobal.UnregisterEventDelegate(WTEventType.ChangePoint, ControlPoint);
    }
    public void ControlHPSlider(int hp)
    {
        WTMain main = WTMain.Instance;
        hpSlider.value = hp;
        hpTMP.SetText(main.playerData.currentHP + "/" + main.playerData.playerAb.maxHP);
    }

    public void ControlMaxHPSlider(int hp)
    {
        WTMain main = WTMain.Instance;
        hpSlider.maxValue = hp;
        hpSlider.value = main.playerData.currentHP;
        hpTMP.SetText(main.playerData.currentHP + "/" + main.playerData.playerAb.maxHP);
    }

    public void ControlGold(int gold)
    {
        WTMain main = WTMain.Instance;
        main.playerData.gold += gold;
        goldTMP.SetText(main.playerData.gold.ToString());
    }

    public void ControlPoint(int point)
    {
        WTMain main = WTMain.Instance;
        main.playerData.point += point;
        pointTMP.SetText(main.playerData.point.ToString());
    }
    private void ResetSlider()
    {
        //daySlider.value = 0;
        hpSlider.maxValue = WTConstants.MaxHP;
        hpSlider.value = WTConstants.MaxHP;
        //daySlider.maxValue = WTConstants.TotalStageTime;
    }

}
