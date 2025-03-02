using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class WTMain : MonoBehaviour
{
    public BasePlayer SpawnPlayerCharacter(ushort TID)
    {
        WTWraithStatTemplate pc_temp = GetPlayerCharacterTemplate(TID);
        if (pc_temp == null) return null;

        Debug.Log("SpawnCharacter");

        if (LoadPlayerCharacterTemplateData(pc_temp))
        {

        }
        return null;
    }

    public bool LoadPlayerCharacterTemplateData(WTWraithStatTemplate pc_template)
    {
        if (pc_template != null)
        {
            if (pc_template.basePrefab == null && pc_template.basePrefabName != "")
            {
                pc_template.basePrefab = (GameObject)Resources.Load(pc_template.basePrefabName);
            }

            return true;
        }
        return false;
    }

}
