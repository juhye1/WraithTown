using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Slot_Unit : MonoBehaviour
{
    public TextMeshProUGUI unitName;
    public SkeletonGraphic unitSkel;

    public void SetUnit(WTSupportUnitTemplate temp)
    {
        if(temp.synergy_id == 10101)
        {
            switch(temp.trait_id)
            {
                case 10201:
                   // unitSkel.AnimationState.SetAnimation("Fire");
                    //흙
                    break;
                case 10210:
                    //달
                    break;
                case 10204:
                    //불
                    break;
            }
            //귀신
        }
        else
        {
            //유령
        }
    }
}
