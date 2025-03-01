using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Slot_Card : MonoBehaviour
{
    public TextMeshProUGUI descTMP;
    public TextMeshProUGUI pointTMP;

    public void SetCardData(WTCardData data)
    {
        descTMP.SetText(data.card_des);
        pointTMP.SetText(data.card_coin_cost.ToString());
    }
}
