using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WTSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnTr;
    [SerializeField]
    private int maxCount;
    [SerializeField]
    private int perCount;

    public void Init()
    {
        gameObject.SetActive(true);
        StartCoroutine(MonsterSpawner());
    }

    IEnumerator MonsterSpawner()
    {
        WaitUntil until = new WaitUntil(() => WTPoolManager.Instance.isInit);
        WaitUntil waitSummon = new WaitUntil(() => BasePlayer.Instance.isPlaying);
        yield return until;
        while(true)
        {
            yield return waitSummon;
            int time = RandomSpawn();
            yield return new WaitForSeconds(time);
        }
    }

    public int RandomSpawn()
    {
        var stageData = WTMain.Instance.dicStageData[WTMain.Instance.playerData.stageID];
        float rate = 0;
        if(WTMain.Instance.playerData.remainTimes != 0)
            rate = WTMain.Instance.playerData.remainTimes / stageData.total_stage_time * 100 ;
        //� �ֵ鸸 ��ȯ �������� Ȯ��
        List<WTEnemyUnitStatsTemplate> list = new List<WTEnemyUnitStatsTemplate>();
        foreach (var data in WTMain.Instance.dicEnemyUnitStatsTemplate)
        {
            if (rate >= data.Value.spawn_start_time && rate <= data.Value.spawn_end_time)
            {
                list.Add(data.Value);
                Debug.LogWarning(data);
            }
        }

        float totalWeight = 0;
        foreach (var data in list) 
        {
            totalWeight += data.spawn_weight;
        }
        Debug.LogWarning(totalWeight + "�Ѱ���ġ");
        int count = 0;
        foreach (var data in list)
        {
            count = Mathf.RoundToInt((data.spawn_weight / totalWeight) * stageData.EnemiesPerWave);
            Debug.LogWarning((data.spawn_weight / totalWeight) + "����ġ ����");
            Debug.LogWarning(stageData.EnemiesPerWave + "�ִ� ������");
            Debug.LogWarning(count + "���� ��ȯ");
            for(int i = 0; i < count; i++)
            {
                var obj = WTPoolManager.Instance.SpawnQueue<BaseEnemy>("NormalEny");
                var idx = Random.Range(0, spawnTr.Length);
                obj.transform.position = spawnTr[idx].position;
                obj.Setup(data);
            }
        }
        return stageData.SpawnRate;
    }
}
