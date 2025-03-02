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
            GameObject go = Instantiate(pc_temp.basePrefab, Vector2.zero, Quaternion.identity);
            playerData = new WTGameData();
            WTWraithStatTemplate temp = GetPlayerCharacterTemplate(TID);
            playerData.playerAb = new WTPlayerAbility(temp); // 생성자에서 temp 값 넣어줌
            playerData.day = 1;
            playerData.currentHP = temp.hp;
            playerData.stageID = WTConstants.StartStageID;
            ChangeGameState(WTGameState.Game);
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
