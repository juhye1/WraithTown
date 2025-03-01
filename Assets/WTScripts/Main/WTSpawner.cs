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
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        StartCoroutine(MonsterSpawner());
    }

    IEnumerator MonsterSpawner()
    {
        WaitUntil until = new WaitUntil(() => WTPoolManager.Instance.isInit);
        yield return until;
        while(BasePlayer.Instance.isPlaying)
        {
            MonsterSpawn();
            yield return new WaitForSeconds(3f);
        }
    }

    public void MonsterSpawn()
    {
        for(int i = 0; i <= perCount / spawnTr.Length; i++)
        {
            var obj = WTPoolManager.Instance.SpawnQueue<BaseEnemy>("NormalEny");
            var idx = Random.Range(0, spawnTr.Length);
            obj.transform.position = spawnTr[idx].position;
        }
    }
}
