using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public void Setup()
    {
        gameObject.SetActive(true);
        MonsterReset();
        StopAllCoroutines();
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
            float time = RandomSpawn();
            yield return new WaitForSeconds(time);
        }
    }

    private void MonsterReset()
    {
        WTPoolManager pm = WTPoolManager.Instance;
        Queue<ObjectPoolBase> pool = pm.qPools["NormalEny"];
        for(int i = 0; i < pool.Count; ++i)
        {
            ObjectPoolBase d = pool.Dequeue();
            BaseEnemy enemy = d as BaseEnemy;
            enemy.isDead = false;
            enemy.gameObject.SetActive(false);
            d.Release();
        }
    }


    private WTEnemyUnitStatsTemplate GetEnemyTemplate(List<WTEnemyUnitStatsTemplate> list, float totalWeight)
    {
        WTEnemyUnitStatsTemplate temp = null;
        int totalIntWeight = (int)(totalWeight * 100);
        int val = Utils.GetRandomNum(totalIntWeight);
        int weight = 0;
        for (int i = 0; i < list.Count; ++i)
        {
            WTEnemyUnitStatsTemplate t = list[i];
            weight = weight + (int)(t.spawn_weight * 100);
            if (val <= weight)
            {
                temp = t;
                return temp;
            }
        }
        return temp;
    }

    public float RandomSpawn()
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

        //Debug.LogWarning(totalWeight + "�Ѱ���ġ");
        int count = 0;
        WTPoolManager pm = WTPoolManager.Instance;
        Queue<ObjectPoolBase> pool = pm.qPools["NormalEny"];
        for (int i = 0; i < stageData.EnemiesPerWave; i++)
        {
            WTEnemyUnitStatsTemplate temp = GetEnemyTemplate(list, totalWeight);
            BaseEnemy obj = null;
            if (pool.Count > 50)
            {
                obj = pool.Dequeue() as BaseEnemy;
            }
            else
            {
                obj = WTPoolManager.Instance.SpawnQueue<BaseEnemy>("NormalEny");
            }
            var idx = Random.Range(0, spawnTr.Length);
            obj.transform.position = spawnTr[idx].position;
            obj.Setup(temp);

        }


        //foreach (var data in list)
        //{
        //    float d = (data.spawn_weight / totalWeight) * stageData.EnemiesPerWave;
        //    //Debug.Log(d);
        //    count = Mathf.RoundToInt(d);
        //    //Debug.Log(count);
        //    //Debug.LogWarning((data.spawn_weight / totalWeight) + "����ġ ����");
        //    //Debug.LogWarning(stageData.EnemiesPerWave + "�ִ� ������");
        //    //Debug.LogWarning(count + "���� ��ȯ");
        //    //for (int i = 0; i < count; i++)
        //    for (int i = 0; i < stageData.EnemiesPerWave; i++)
        //    {
        //        BaseEnemy obj = null;
        //        if (pool.Count > 50)
        //        {
        //            obj = pool.Dequeue() as BaseEnemy;
        //        }
        //        else
        //        {
        //            obj = WTPoolManager.Instance.SpawnQueue<BaseEnemy>("NormalEny");
        //        }
        //        var idx = Random.Range(0, spawnTr.Length);
        //        obj.transform.position = spawnTr[idx].position;
        //        obj.Setup(data);
        //        Debug.Log("스폰");

        //    }
        //}
        return stageData.SpawnRate;
    }
}
