using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Slot_TMPSupportUnit : MonoBehaviour
{
    public TextMeshProUGUI nameTMP;
    public TextMeshProUGUI descTMP;

    public void SetData(WTSupportUnitAbilityTemplate temp, SupportUnitCount count)
    {
        nameTMP.SetText(temp.support_ability_name);
        string d = ReturnChangeString(temp, count);
        descTMP.SetText(d);
    }

    private string ReturnChangeString(WTSupportUnitAbilityTemplate temp, SupportUnitCount count)
    {
        string d = string.Empty;
        string o = temp.support_ability;
        string percent = string.Empty;
        float stat = temp.support_ability_stat;
        if(count.unitCount>1)
        {
            stat = stat + (stat * count.unitCount);
        }

        if (temp.support_ability_stat <= 1)
        {
            percent = "%";
        }
        o = o.Replace("0", stat + percent);
        if (temp.support_unit_id == 12001)
        {
            o = o.Replace("0", stat + percent);
        }
        return o;
    }
}
