using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
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
    public SkeletonGraphic profileSkel_M;
    public SkeletonGraphic profileSkel_K;
    public SkeletonDataAsset kebi;
    public SkeletonDataAsset miho;

    [Header("ETC")]
    public Slot_Synergy slotSynergy;
    public RectTransform slotParent;
    private List<Slot_Synergy> slots = new();
    private Dictionary<ushort, Slot_Synergy> dicSlot = new();
    [NonSerialized] public Slot_Synergy currentSlot;
    public Image vignetteImage;
    //public Slider daySlider;

    private void Start()
    {
        RT = transform as RectTransform;
        ResetSlider();
        WTMain main = WTMain.Instance;
        ControlGold(0);
        ControlPoint(0);
        SpawnSynergySlots();
        SetProfile(0);
        WTGlobal.CallEventDelegate(WTEventType.PlayerSpawn, 0);
    }
    private void OnEnable()
    {
        WTGlobal.RegisterEventDelegate(WTEventType.PlayerHPControl, ControlHPSlider);
        WTGlobal.RegisterEventDelegate(WTEventType.PlayerMaxHPControl, ControlMaxHPSlider);

        WTGlobal.RegisterEventDelegate(WTEventType.PlayerSpawn, SetProfile);
        WTGlobal.RegisterEventDelegate(WTEventType.ChangeGold, ControlGold);
        WTGlobal.RegisterEventDelegate(WTEventType.ChangePoint, ControlPoint);
        WTGlobal.RegisterEventDelegate(WTEventType.AddSynergy, AddSynergy);
        WTGlobal.RegisterEventDelegate(WTEventType.StageEnd, StageEnd);
    }
    private void OnDisable()
    {
        WTGlobal.UnregisterEventDelegate(WTEventType.PlayerHPControl, ControlHPSlider);
        WTGlobal.UnregisterEventDelegate(WTEventType.PlayerMaxHPControl, ControlMaxHPSlider);

        WTGlobal.UnregisterEventDelegate(WTEventType.PlayerSpawn, SetProfile);
        WTGlobal.UnregisterEventDelegate(WTEventType.ChangeGold, ControlGold);
        WTGlobal.UnregisterEventDelegate(WTEventType.ChangePoint, ControlPoint);
        WTGlobal.UnregisterEventDelegate(WTEventType.AddSynergy, AddSynergy);
        WTGlobal.UnregisterEventDelegate(WTEventType.StageEnd, StageEnd);
    }

    private void StageEnd(int val)
    {
        bool enable = val == 0 ? true : false;
        vignetteImage.enabled = enable;
    }

    private void SetProfile(int d)
    {
        WTMain main = WTMain.Instance;
        if(main.playerData.userUnitId == WTConstants.UnitIDKebi)
        {
            profileSkel_K.enabled = true;
        }
        else
        {
            profileSkel_M.enabled = true;
        }
        //    profileSkel_M
        //profileSkel.skeletonDataAsset = main.playerData.userUnitId == WTConstants.UnitIDKebi ? kebi : miho;
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
        WTMain main = WTMain.Instance;

        hpSlider.maxValue = main.playerData.playerAb.maxHP;
        hpSlider.value = main.playerData.currentHP;
        ControlHPSlider(main.playerData.currentHP);
        //daySlider.maxValue = WTConstants.TotalStageTime;
    }

}
