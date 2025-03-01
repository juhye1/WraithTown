using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : SerializedMonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    [Tooltip("�� �̵� �� : true ���ı�/ false �ı�")]
    [SerializeField] protected bool isDontDestroyOnLoad = true;
    protected static bool isUILoading = false;
    public static T Instance
    {
        get
        {
            if (!isUILoading)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject go = new GameObject(typeof(T).Name);
                        instance = go.AddComponent<T>();
                    }
                }
            }
            return instance;
        }
    }

    //�Ŵ������� Awakeȣ�� �� Base�� ȣ�� �Ұ�!!
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            if (isDontDestroyOnLoad)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance != null)
            Destroy(gameObject);
    }
}
