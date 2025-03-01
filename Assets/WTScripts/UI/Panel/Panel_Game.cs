using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Game : MonoBehaviour
{
    [Header("TMP")]
    public TextMeshProUGUI dayTMP;
    public TextMeshProUGUI titleTMP;
    public TextMeshProUGUI pointTMP;
    public TextMeshProUGUI goldTMP;
    
    [Header("Slider")]
    public Slider hpSlider;
    public Slider daySlider;


    private void OnEnable()
    {
        WTGlobal.RegisterEventDelegate(WTEventType.PlayerHPControl, ControlHP);
    }

    private void OnDisable()
    {
        WTGlobal.UnregisterEventDelegate(WTEventType.PlayerHPControl, ControlHP);
    }
    public void ControlHP(int hp)
    {
        hpSlider.value = hp;
    }

}
