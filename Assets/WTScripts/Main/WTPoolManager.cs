using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WTPoolManager : Singleton<WTPoolManager>
{
    public Transform[] poolParents;
    public Dictionary<string, Queue<ObjectPoolBase>> qPools = new Dictionary<string, Queue<ObjectPoolBase>>();
    public Dictionary<string, List<ObjectPoolBase>> lPools = new Dictionary<string, List<ObjectPoolBase>>();
    public List<ObjectPoolBase> prefabQList = new();
    public List<ObjectPoolBase> prefabLList = new();
    public bool isInit = false;

    #region 초기화
    [ContextMenu("초기화")]
    public void Init()
    {
        //큐를 이용한 풀링
        foreach (var data in prefabQList)
        {
            WTResourcesManager.Instance.LoadAsset<ObjectPoolBase>(data.rCode, eAddressableType.prefab, (obj) =>
            {
                data.prefab = obj;
            });

            if (data.isUI)
            {
                data.parent = new GameObject(data.rCode + "parent", typeof(RectTransform)).transform;
                data.parent.SetParent(poolParents[1]);
                data.parent.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                var canvas = data.parent.gameObject.AddComponent<Canvas>();
                canvas.overrideSorting = true;
                canvas.sortingOrder = data.sort;
            }
            else
            {
                data.parent = new GameObject(data.rCode + "parent").transform;
                data.parent.SetParent(poolParents[0]);
            }

            Queue<ObjectPoolBase> queue = new Queue<ObjectPoolBase>();
            qPools.Add(data.rCode, queue);
            for (int i = 0; i < data.count; i++)
            {
                var obj = Instantiate(data.prefab, data.parent);
                obj.name = obj.name.Replace("(Clone)", "");
                obj.parent = data.parent;
                obj.Init();
                queue.Enqueue(obj);
            }
        }

        //리스트를 이용한 풀링
        foreach (var data in prefabLList)
        {
            WTResourcesManager.Instance.LoadAsset<ObjectPoolBase>(data.rCode, eAddressableType.prefab, (obj) =>
            {
                data.prefab = obj;
            });
            data.parent = new GameObject(data.rCode + "parent").transform;

            if (data.isUI)
            {
                data.parent = new GameObject(data.rCode + "parent", typeof(RectTransform)).transform;
                data.parent.SetParent(poolParents[1]);
                data.parent.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                var canvas = data.parent.gameObject.AddComponent<Canvas>();
                canvas.overrideSorting = true;
                canvas.sortingOrder = data.sort;
            }
            else
            {
                data.parent = new GameObject(data.rCode + "parent").transform;
                data.parent.SetParent(poolParents[0]);
            }

            var list = new List<ObjectPoolBase>();
            list.Capacity = data.count;
            for (int i = 0; i < data.count; i++)
            {
                var obj = Instantiate(data.prefab, data.parent);
                obj.name = obj.name.Replace("(Clone)", "");
                obj.parent = data.parent;
                obj.Init();
                list.Add(obj);
            }
            lPools.Add(data.rCode, list);
        }

        isInit = true;
    }
    #endregion

    #region 스폰 큐
    public T SpawnQueue<T>(string rcode) where T : ObjectPoolBase
    {
        if (qPools[rcode].Count == 0)
        {
            var prefab = prefabQList.Find(obj => obj.rCode == rcode);
            for (int i = 0; i < prefab.count; i++)
            {
                var obj = Instantiate(prefab.prefab, prefab.parent);
                obj.name = obj.name.Replace("(Clone)", "");
                qPools[rcode].Enqueue(obj);
            }
        }
        var retObj = (T)qPools[rcode].Dequeue();
        retObj.SetActive(true);
        retObj.Setup();
        return retObj;
    }

    public T Spawn<T>(string rcode, Vector3 position) where T : ObjectPoolBase
    {
        var obj = SpawnQueue<T>(rcode);
        obj.transform.position = position;
        return obj;
    }

    public T Spawn<T>(string rcode, Vector3 position, Transform parent) where T : ObjectPoolBase
    {
        var obj = Spawn<T>(rcode, position);
        obj.transform.parent = parent;
        return obj;
    }

    public T Spawn<T>(string rcode, Vector3 position, Quaternion rotation, Transform parent) where T : ObjectPoolBase
    {
        var obj = Spawn<T>(rcode, position, parent);
        obj.transform.rotation = rotation;
        return obj;
    }
    #endregion
    #region 릴리즈
    public void Release(ObjectPoolBase item)
    {
        item.SetActive(false);
        var prefab = prefabQList.Find(obj => obj.rCode == item.name);
        item.transform.SetParent(prefab.parent);
        qPools[item.name].Enqueue(item);
    }

    public void AllRelease(string rCode)
    {
        if (qPools.ContainsKey(rCode))
        {
            for (int i = 0; i < qPools.Count; i++)
            {
                var obj = qPools[rCode].Dequeue();
                if (obj.gameObject.activeSelf)
                    obj.SetActive(false);
                qPools[rCode].Enqueue(obj);
            }
        }
        if (lPools.ContainsKey(rCode))
        {
            for (int i = 0; i < lPools.Count; i++)
            {
                if (lPools[rCode][i].gameObject.activeSelf)
                    lPools[rCode][i].SetActive(false);
            }
        }
    }

    public void DestoryPool(string rCode)
    {
        if (lPools.ContainsKey(rCode))
        {
            for (int i = 0; i < lPools.Count; i++)
            {
                Destroy(lPools[rCode][i].gameObject);
            }
        }
    }
    #endregion

    #region 스폰 리스트
    //가져옴과 동시에 넣어주고 부족 시 추가생성X
    public T SpawnFromPool<T>(string rcode) where T : ObjectPoolBase
    {
        if (!qPools.ContainsKey(rcode)) return default;

        ObjectPoolBase obj = qPools[rcode].Dequeue();
        if (obj.gameObject.activeSelf)
        {
            obj.Init();
        }
        qPools[rcode].Enqueue(obj);
        obj.Setup();
        return (T)obj;
    }


    //리스트풀에서 인덱스로 해당 번쨰를 가져옴
    public T SpawnList<T>(string rcode, int index = 0) where T : ObjectPoolBase
    {
        if (lPools[rcode].Count == 0 || lPools[rcode].Count <= index)
        {
            var prefab = prefabLList.Find(obj => obj.rCode == rcode);
            for (int i = 0; i < prefab.count; i++)
            {
                var obj = Instantiate(prefab.prefab, prefab.parent);
                obj.name.Replace("(Clone)", "");
                lPools[rcode].Add(obj);
            }
        }
        var retObj = (T)lPools[rcode][index];
        retObj.Setup();
        return retObj;
    }

    public T SpawnList<T>(string rcode, Vector3 position, int index = 0) where T : ObjectPoolBase
    {
        if (lPools[rcode].Count == 0 || lPools[rcode].Count <= index)
        {
            var prefab = prefabLList.Find(obj => obj.rCode == rcode);
            int addPrefab = index - prefab.count;
            prefab.count = index + 1;
            for (int i = 0; i < addPrefab; i++)
            {
                var obj = Instantiate(prefab.prefab, prefab.parent);
                obj.name.Replace("(Clone)", "");
                lPools[rcode].Add(obj);
            }
        }
        var retObj = (T)lPools[rcode][index];
        retObj.transform.position = position;
        retObj.Setup();
        return retObj;
    }
    #endregion
}
