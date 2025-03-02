using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour
{
    private BasePlayer player => BasePlayer.Instance;
    public WTWraithStatTemplate status;
    public void Init()
    {
        status = WTMain.Instance.GetPlayerCharacterTemplate((ushort)WTMain.Instance.playerData.userUnitId);
    }

}
