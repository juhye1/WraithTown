using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_SelectCharacter : MonoBehaviour
{
    public void OnClickBtn_Miho()
    {
        SpawnCharacter(WTConstants.UnitIDMiho);
    }

    public void OnClickBtn_Kebi()
    {
        SpawnCharacter(WTConstants.UnitIDKebi);
    }

    private void SpawnCharacter(ushort id)
    {
        WTMain main = WTMain.Instance;
        main.SpawnPlayerCharacter(id);
    }
}
