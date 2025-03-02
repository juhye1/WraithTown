using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WTUIPrefabContainer : MonoBehaviour
{
    private static WTUIPrefabContainer _instance;
    public static WTUIPrefabContainer Instance
    {
        get
        {
            return _instance;
        }
    }

    [Header("Panel")]
    public GameObject panelLobby;
    public GameObject panelLoading;
    public GameObject panelGame;
    public GameObject panelDay;
    public GameObject panelShop;
    public GameObject panelPause;
    public GameObject panelGameOver;
    public GameObject panelSelectCharacter;
    public GameObject panelStory;

    public void Awake()
    {
        _instance = this;
    }
}
