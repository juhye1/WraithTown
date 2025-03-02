using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Unit : MonoBehaviour
{
    public TextMeshProUGUI unitName;
    public TextMeshProUGUI unitCount;
    public SkeletonGraphic unitSkel;
    private SupportUnitCount currentUnitData;

    public void ChangeUnitPosition()
    {

    }

    public void SetUnit(WTSupportUnitTemplate temp, SkeletonDataAsset skelData, SupportUnitCount unitData, string animaionKey)
    {
        if (currentUnitData != null)
        {
            if (unitData == currentUnitData)
            {
                return;
            }
        }
        currentUnitData = unitData;
        unitSkel.skeletonDataAsset = skelData;
        unitCount.SetText(unitData.unitCount.ToString());
        unitName.SetText(temp.support_unit_name);
        unitSkel.AnimationState.SetAnimation(0, animaionKey, true);
    }
}
