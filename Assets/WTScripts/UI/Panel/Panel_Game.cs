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
    
    [Header("Slider")]
    public Slider hpSlider;
    //public Slider daySlider;

    private void Awake()
    {
        RT = transform as RectTransform;
        ResetSlider();
    }
    private void OnEnable()
    {
        WTGlobal.RegisterEventDelegate(WTEventType.PlayerHPControl, ControlHPSlider);
        WTGlobal.RegisterEventDelegate(WTEventType.ChangeGold, ControlGold);
    }
    private void OnDisable()
    {
        WTGlobal.UnregisterEventDelegate(WTEventType.PlayerHPControl, ControlHPSlider);
        WTGlobal.UnregisterEventDelegate(WTEventType.ChangeGold, ControlGold);
    }
    public void ControlHPSlider(int hp)
    {
        WTMain main = WTMain.Instance;
        main.playerData.hp += hp;
        hpSlider.value = hp;
    }

    public void ControlGold(int gold)
    {
        WTMain main = WTMain.Instance;
        main.playerData.gold += gold;
        goldTMP.SetText(main.playerData.gold.ToString());
    }
    private void ResetSlider()
    {
        //daySlider.value = 0;
        hpSlider.value = WTConstants.MaxHP;
        //daySlider.maxValue = WTConstants.TotalStageTime;
        hpSlider.maxValue = WTConstants.MaxHP;
    }

}
