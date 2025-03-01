using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Slot_TMPSupportUnit : MonoBehaviour
{
    public TextMeshProUGUI nameTMP;
    public TextMeshProUGUI descTMP;

    public void SetData(WTSupportUnitAbilityTemplate temp)
    {
        nameTMP.SetText(temp.support_ability_name);
        descTMP.SetText(temp.support_ability);
    }
}
