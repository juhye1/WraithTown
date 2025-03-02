using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class WTMain : MonoBehaviour
{
    [NonSerialized] public WTSpawner spawner;

    public void SpawnSpawner()
    {
        var _spawner = Resources.Load<WTSpawner>("Spawner");
        if (_spawner != null)
        {
            spawner = Instantiate(_spawner); // 씬에 인스턴스 생성
            spawner.gameObject.SetActive(true);
            spawner.Init();
        }
    }
}
