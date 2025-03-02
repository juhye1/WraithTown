using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolBase : MonoBehaviour
{

    public ObjectPoolBase prefab;
    public Transform parent;
    public string rCode;
    public int count;
    public bool isUI;
    public int sort = 0;
    private void Awake()
    {
        Init();
        Setup();
    }

    public virtual void Init()
    {
        parent = WTPoolManager.Instance.gameObject.transform;
        SetActive(false);
    }

    public virtual void Setup()
    {
        SetActive(true);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public virtual void Release()
    {
        WTPoolManager.Instance.Release(this);
    }
}

