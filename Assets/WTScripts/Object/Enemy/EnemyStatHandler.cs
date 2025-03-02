using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatHandler : MonoBehaviour
{
    public WTEnemyUnitStatsTemplate template;
    public int hp;
    public void Init(WTEnemyUnitStatsTemplate temp)
    {
        template = temp; 
        hp = temp.hp;
    }
}
