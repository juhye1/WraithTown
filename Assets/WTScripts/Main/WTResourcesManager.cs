using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class WTResourcesManager : Singleton<WTResourcesManager>
{
    public bool isAutoLoading = false;
    public bool isInit = false;

    public Dictionary<eAddressableType, Dictionary<string, AddressableMap>> addressableMap
        = new Dictionary<eAddressableType, Dictionary<string, AddressableMap>>();

    void Start()
    {
        LoadAddressable();
    }

    public void LoadAddressable()
    {
        StartCoroutine(CLoadAddressable());
    }

    public IEnumerator CLoadAddressable()
    {
        yield return Addressables.InitializeAsync();
        Debug.Log("어드레서블 초기화 완료");
        var handle = Addressables.DownloadDependenciesAsync("InitDownload");
        Debug.Log("다운로드 시작");

        while (!handle.IsDone)
        {
            Debug.Log($"다운로드 진행률: {handle.PercentComplete * 100}%");
            //TitleManager.Instance.titleScene.SetGuageBar(handle.PercentComplete);
            yield return null;
        }
        //TitleManager.Instance.titleScene.SetGuageBar(1f);
        Debug.Log("다운로드 완료 상태 확인");

        switch (handle.Status)
        {
            case AsyncOperationStatus.None:
                break;
            case AsyncOperationStatus.Succeeded:
                Debug.Log("다운로드 성공!");
                break;
            case AsyncOperationStatus.Failed:
                Debug.Log("다운로드 실패 : " + handle.OperationException.Message);
                Debug.LogError(handle.OperationException.ToString());
                break;
            default:
                break;
        }
        Addressables.Release(handle);
        InitAddressableMap();
    }

    public IEnumerator SetProgress(AsyncOperationHandle handle)
    {
        while (!handle.IsDone)
        {
            //UILoading.instance.SetProgress(handle.GetDownloadStatus().Percent, "Resource Download...");
            yield return new WaitForEndOfFrame();
        }
        //UILoading.instance.SetProgress(1);

    }

    private void InitAddressableMap()
    {
        //TitleManager.Instance.titleScene.SetGuageBar(0);
        Debug.Log("startad");
        int adIndex = 0;
        Addressables.LoadAssetsAsync<TextAsset>("AddressableMap", (text) =>
        {
            var map = JsonUtility.FromJson<AddressableMapData>(text.text);
            var key = eAddressableType.prefab;
            Dictionary<string, AddressableMap> mapDic = new Dictionary<string, AddressableMap>();
            foreach (var data in map.list)
            {
                key = data.addressableType;
                if (!mapDic.ContainsKey(data.key))
                    mapDic.Add(data.key, data);
            }
            if (!addressableMap.ContainsKey(key)) addressableMap.Add(key, mapDic);
            ++adIndex;
            //TitleManager.Instance.titleScene.SetGuageBar(++adIndex / (int)eAddressableType.max - 1);
            if (adIndex == (int)eAddressableType.max)
            {
                Debug.Log(adIndex);
                isInit = true;
                WTPoolManager.Instance.Init();
            }
        });
        //TitleManager.Instance.titleScene.SetGuageBar(1f);
        
    }

    public List<string> GetPaths(string key, eAddressableType addressableType, eAssetType assetType)
    {
        var keys = new List<string>(addressableMap[addressableType].Keys);
        keys.RemoveAll(obj => !obj.Contains(key));
        List<string> retList = new List<string>();
        keys.ForEach(obj =>
        {
            if (addressableMap[addressableType][obj].assetType == assetType)
                retList.Add(addressableMap[addressableType][obj].path);
        });
        return retList;
    }

    public string GetPath(string key, eAddressableType addressableType)
    {
        var map = addressableMap[addressableType][key];
        return map.path;
    }

    public void LoadAssets<T>(string key, eAddressableType addressableType, eAssetType assetType, Action<List<T>> callback)
    {
        StartCoroutine(CLoadAssets(key, addressableType, assetType, callback));
    }

    IEnumerator CLoadAssets<T>(string key, eAddressableType addressableType, eAssetType assetType, Action<List<T>> callback)
    {
        var paths = GetPaths(key, addressableType, assetType);
        List<T> retList = new List<T>();
        foreach (var path in paths)
        {
            yield return CLoadAsset<T>(path, obj =>
            {
                retList.Add(obj);
            });
        }
        yield return new WaitUntil(() => paths.Count == retList.Count);
        callback.Invoke(retList);
    }

    public void LoadAsset<T>(string key, eAddressableType addressableType, Action<T> callback)
    {
        var path = GetPath(key, addressableType);
        LoadAsset<T>(path, callback);
    }

    public void LoadAsset<T>(string path, Action<T> callback)
    {
        StartCoroutine(CLoadAsset(path, callback));
    }

    public IEnumerator CLoadAsset<T>(string path, Action<T> callback)
    {
        if (path.Contains(".prefab") && typeof(T) != typeof(GameObject) || path.Contains("UI/"))
        {
            var handler = Addressables.LoadAssetAsync<GameObject>(path);
            handler.Completed += (op) =>
            {
                callback.Invoke(op.Result.GetComponent<T>());
            };
            yield return handler;
        }
        else
        {
            var handler = Addressables.LoadAssetAsync<T>(path);
            handler.Completed += (op) =>
            {
                callback.Invoke(op.Result);
            };
            yield return handler;
        }
    }
}

