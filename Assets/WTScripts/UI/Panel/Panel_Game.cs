using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Game : MonoBehaviour
{
   [NonSerialized] public RectTransform RT;
    [Header("TMP")]
    public TextMeshProUGUI pointTMP;
    public TextMeshProUGUI goldTMP;
    public TextMeshProUGUI hpTMP;
    
    [Header("Slider")]
    public Slider hpSlider;
    public SkeletonGraphic profileSkel;
    public SkeletonDataAsset kebi;
    public SkeletonDataAsset miho;

    [Header("ETC")]
    public Slot_Synergy slotSynergy;
    public RectTransform slotParent;
    private List<Slot_Synergy> slots = new();
    private Dictionary<ushort, Slot_Synergy> dicSlot = new();
    //public Slider daySlider;

    private void Awake()
    {
        RT = transform as RectTransform;
        ResetSlider();
        WTMain main = WTMain.Instance;
        ControlGold(100);
        ControlPoint(100);
        SpawnSynergySlots();
    }
    private void OnEnable()
    {
        WTGlobal.RegisterEventDelegate(WTEventType.PlayerHPControl, ControlHPSlider);
        WTGlobal.RegisterEventDelegate(WTEventType.PlayerSpawn, SetProfile);
        WTGlobal.RegisterEventDelegate(WTEventType.PlayerMaxHPControl, ControlMaxHPSlider);
        WTGlobal.RegisterEventDelegate(WTEventType.ChangeGold, ControlGold);
        WTGlobal.RegisterEventDelegate(WTEventType.ChangePoint, ControlPoint);
        WTGlobal.RegisterEventDelegate(WTEventType.AddSynergy, AddSynergy);
    }
    private void OnDisable()
    {
        WTGlobal.UnregisterEventDelegate(WTEventType.PlayerHPControl, ControlHPSlider);
        WTGlobal.UnregisterEventDelegate(WTEventType.PlayerSpawn, SetProfile);
        WTGlobal.UnregisterEventDelegate(WTEventType.PlayerMaxHPControl, ControlMaxHPSlider);
        WTGlobal.UnregisterEventDelegate(WTEventType.ChangeGold, ControlGold);
        WTGlobal.UnregisterEventDelegate(WTEventType.ChangePoint, ControlPoint);
        WTGlobal.UnregisterEventDelegate(WTEventType.AddSynergy, AddSynergy);
    }

    private void SetProfile(int d)
    {
        WTMain main = WTMain.Instance;
        profileSkel.skeletonDataAsset = main.playerData.userUnitId == WTConstants.UnitIDKebi ? kebi : miho;
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
    public void AddSynergy(int val)
    {
        WTMain main = WTMain.Instance;
        List<SupportUnitCount> c = main.playerData.supportUnits;
        SupportUnitCount u = null;
        for (int i = 0; i < c.Count; ++i)
        {
            if (c[i].unitID == val)
            {
                u = c[i];
                break;
            }
        }
        WTSupportUnitTemplate unitTemp = main.GetSupportUnitTemplate(u.unitID);
        Slot_Synergy s1 = dicSlot[unitTemp.synergy_id];
        Slot_Synergy s2 = dicSlot[unitTemp.trait_id];
        s1.gameObject.SetActive(true);
        s2.gameObject.SetActive(true);
        Sprite sprite1 = main.GetSynergySprite(unitTemp.synergy_id, u.unitCount);
        Sprite sprite2 = main.GetSynergySprite(unitTemp.trait_id, u.unitCount);
        s1.SetData(sprite1, main.GetTotalSynergeTemplate(unitTemp.synergy_id), u.unitCount);
        s2.SetData(sprite2, main.GetTotalSynergeTemplate(unitTemp.trait_id), u.unitCount);



        //for (int i = 0; i < c.Count; ++i)
        //{
        //    WTSupportUnitTemplate unitTemp = main.GetSupportUnitTemplate(c[i].unitID);
        //    Slot_Synergy s1 = dicSlot[unitTemp.synergy_id];
        //    Slot_Synergy s2 = dicSlot[unitTemp.trait_id];
        //    s1.gameObject.SetActive(true);
        //    s2.gameObject.SetActive(true);
        //    Sprite sprite1 = main.GetSynergySprite(unitTemp.synergy_id, c[i].unitCount);
        //    Sprite sprite2 = main.GetSynergySprite(unitTemp.trait_id, c[i].unitCount);
        //    s1.SetData(sprite1, main.GetTotalSynergeTemplate(unitTemp.synergy_id));
        //    s2.SetData(sprite2, main.GetTotalSynergeTemplate(unitTemp.trait_id));
        //}

    }

    private void SpawnSynergySlots()
    {
        WTMain main = WTMain.Instance;
        for(int i=0; i<7; ++i)
        {
            Slot_Synergy s = Instantiate(slotSynergy, slotParent);
            slots.Add(s);
            dicSlot.Add(main.synergyIds[i], s);
            s.gameObject.SetActive(false);
        }
    }

    private void ResetSlider()
    {
        //daySlider.value = 0;
        hpSlider.maxValue = WTConstants.MaxHP;
        hpSlider.value = WTConstants.MaxHP;
        //daySlider.maxValue = WTConstants.TotalStageTime;
    }

}
