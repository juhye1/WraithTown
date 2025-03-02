using Sirenix.OdinInspector;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Unit : MonoBehaviour
{
    public Image disablePlayerAbImg;
    public Image disableSupportAbImg;
    public TextMeshProUGUI playerABTMP;
    [Header("GameObject")]
    public GameObject supportUnitTMPBox;
    public GameObject playerABTMPBox;
    public Slot_TMPSupportUnit tmpUnitPrefab;
    private List<Slot_TMPSupportUnit> tmpUnitList = new();
    public RectTransform parentRT;
    private StringBuilder sb = new StringBuilder(20);
    [Header("Unit")]
    public Slot_Unit slotUnit;
    public RectTransform slotParent;
    private List<Slot_Unit> slotUnitList = new();
    public SkeletonDataAsset ghost;
    public SkeletonDataAsset youkai;

    private void Awake()
    {
        SpawnUnitSlots(); // 유닛 얼굴들
        SpawnTMPSuppots(); // tmp 슬롯들
    }
    private void OnEnable()
    {
        ShowPlayerAbility();
        ShowUnits();

    }

    public void ShowUnits()
    {
        WTMain main = WTMain.Instance;
        List<SupportUnitCount> spUnits = main.playerData.supportUnits;
        DisableAllUnitSlots();
        for (int i = 0; i < spUnits.Count; ++i)
        {
            SupportUnitCount c = spUnits[i];
            ushort id = c.unitID;
            Slot_Unit unit = slotUnitList[i];
            unit.gameObject.SetActive(true);
            WTSupportUnitTemplate temp = main.GetSupportUnitTemplate(c.unitID);
            SkeletonDataAsset data = temp.synergy_id == 10101 ? ghost : youkai;
            unit.SetUnit(temp, data, c, ReturnAnimationKey(temp));
            slotUnitList.Add(unit);
        }
    }

    private void SpawnUnitSlots()
    {
        for (int i = 0; i < 8; ++i)
        {
            Slot_Unit unit = Instantiate(slotUnit, slotParent);
            slotUnitList.Add(unit);
        }
        DisableAllUnitSlots();
    }

    private void DisableAllUnitSlots()
    {
        for (int i = 0; i < slotUnitList.Count; ++i)
        {
            slotUnitList[i].gameObject.SetActive(false);
        }
    }

    public void OnClickBtn_ShuffleUnitPos()
    {
        WTGlobal.CallEventDelegate(WTEventType.ChangePoint, -10); // 테스트
        //유닛 위치 랜덤 바꾸기
        WTMain main = WTMain.Instance;
        BasePlayer player = main.player;
        List<SupportUnitCount> spUnits = main.playerData.supportUnits;
        List<ushort> totalSpUnits = main.playerData.totalSupportUnits;
        Utils.Shuffle(player.tiles);
        main.playerData.activeSupportUnits.Clear();
        int j = 0;
        for (int i=0; i<player.tiles.Count; ++i)
        {
            WraithTile t = player.tiles[i];
            if (i < totalSpUnits.Count)
            {
                WTSupportUnitTemplate temp = main.GetSupportUnitTemplate(totalSpUnits[i]);
                main.AddActiveUnit(temp.support_unit_id);
                SkeletonDataAsset d = GetSkeletonData(temp.synergy_id);
                t.SetUnit(d, ReturnAnimationKey(temp));
                j++;
            }
            else
            {
                t.RemoveUnit();
            }
        }

        if(!disableSupportAbImg.enabled)
        {
            ShowSupportAbility();
        }
    }

    public string ReturnAnimationKey(WTSupportUnitTemplate temp)
    {
        string animaionKey = string.Empty;
        if (temp.synergy_id == 10101)
        {
            switch (temp.trait_id)
            {
                case 10201:
                    animaionKey = "Soil_01";
                    //흙
                    break;
                case 10210:
                    animaionKey = temp.support_unit_id == 10002 ? "Moon_01" : "Moon_02";
                    //달
                    break;
                case 10204:
                    animaionKey = "Fire_01";
                    //불
                    break;
            }
            //귀신
        }
        else
        {
            switch (temp.trait_id)
            {
                //금 1, 달2, 물1
                case 10207:
                    animaionKey = "Gold_01";
                    //흙
                    break;
                case 10210:
                    animaionKey = temp.support_unit_id == 10002 ? "Moon_01" : "Moon_02";
                    //달
                    break;
                case 10213:
                    animaionKey = "Water_01";

                    //불
                    break;
            }
            //유령
        }
        return animaionKey;
    }


    private SkeletonDataAsset GetSkeletonData(ushort id)
    {
        SkeletonDataAsset data = id == 10101 ? ghost : youkai;
        return data;
    }

    private void ShowPlayerAbility()
    {
        playerABTMPBox.SetActive(true);
        supportUnitTMPBox.SetActive(false);
        WTMain main = WTMain.Instance;
        WTPlayerAbility ab = main.playerData.playerAb;
        sb.AppendLine(ab.maxHP.ToString());
        sb.AppendLine(ab.hpRecoveryPerSec.ToString());
        sb.AppendLine(ab.damage.ToString());
        sb.AppendLine(ab.moveSpeed.ToString());
        sb.AppendLine(ab.attackSpeed.ToString());
        sb.AppendLine(ab.projectileCount.ToString());
        sb.AppendLine(ab.attackRange.ToString());
        playerABTMP.SetText(sb.ToString());
        sb.Clear();
    }

    private void ShowSupportAbility()
    {
        playerABTMPBox.SetActive(false);
        supportUnitTMPBox.SetActive(true);
        WTMain main = WTMain.Instance;
        List<SupportUnitCount> supList = main.playerData.activeSupportUnits;
        DisableTMPs();
        if (supList.Count > 0)
        {
            for(int i=0; i<supList.Count; ++i)
            {
                Slot_TMPSupportUnit unit = tmpUnitList[i];
                unit.gameObject.SetActive(true);
                WTSupportUnitAbilityTemplate data = main.GetSupportUnitAbilityTemplate(supList[i].unitID);
                unit.SetData(data, supList[i]);
            }
            //유닛 먹으면 보조 기능 띄우기 ~~
        }
    }

    private void DisableTMPs()
    {
        for(int i=0; i<tmpUnitList.Count; ++i)
        {
            tmpUnitList[i].gameObject.SetActive(false);
        }
    }

    private void SpawnTMPSuppots()
    {
        WTMain main = WTMain.Instance;
        for (int i = 0; i < 8; ++i)
        {
            Slot_TMPSupportUnit unit = Instantiate(tmpUnitPrefab, parentRT);
            tmpUnitList.Add(unit);
            tmpUnitList[i].gameObject.SetActive(false);
        }
    }

    public void OnClickBtn_ShowPlayerAbility()
    {
        disablePlayerAbImg.enabled = false;
        disableSupportAbImg.enabled = true;
        ShowPlayerAbility();
    }

    public void OnClickBtn_ShowSupportAbility()
    {
        disablePlayerAbImg.enabled = true;
        disableSupportAbImg.enabled = false;
        ShowSupportAbility();
    }
}
