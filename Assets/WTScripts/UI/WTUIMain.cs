using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WTUIState
{
    None = -1,
    Lobby = 0,
    Loading = 1,
    Story = 2,
    Game = 3,
    Shop = 4,
    SelectCharacter = 5,
    GameOver = 6,
    Pause = 7,
    Count
}

public class WTUIMain : MonoBehaviour
{
    private static WTUIMain _instance;
    public static WTUIMain Instance
    {
        get
        {
            return _instance;
        }
    }
    public RectTransform canvasRT;
    [NonSerialized] public WTUIState uiState = WTUIState.None;
    [NonSerialized] public Panel_Lobby panel_Lobby;
    [NonSerialized] public GameObject[] panels = new GameObject[(int)WTUIState.Count];
    //[NonSerialized] public Panel_Loading panel_Loading;
    [NonSerialized] public Panel_Game panel_Game;

    public void Awake()
    {
        _instance = this;
    }

    public GameObject SpawnUIObject(GameObject uiPrefab)
    {
        GameObject goUIPrefab = Instantiate(uiPrefab);
        goUIPrefab.transform.SetParent(transform);
        (goUIPrefab.transform as RectTransform).offsetMin = Vector2.zero;
        (goUIPrefab.transform as RectTransform).offsetMax = Vector2.zero;
        goUIPrefab.transform.localScale = Vector3.one;
        return goUIPrefab;
    }

    public GameObject SpawnUIObject(GameObject uiPrefab, Transform parentTR)
    {
        GameObject goUIPrefab = Instantiate(uiPrefab);
        goUIPrefab.transform.SetParent(parentTR);
        (goUIPrefab.transform as RectTransform).offsetMin = Vector2.zero;
        (goUIPrefab.transform as RectTransform).offsetMax = Vector2.zero;
        goUIPrefab.transform.localScale = Vector3.one;
        return goUIPrefab;
    }

    public bool ShowConfirmMsg(WTPopUpMsgType popupMsgType)
    {
        WTUIPrefabContainer uiPrefabContainer = WTUIPrefabContainer.Instance;
        //if (panel_ComfirmPopup == null)
        //{
        //    panel_ComfirmPopup = SpawnUIObject(uiPrefabContainer.confirmPopup).GetComponent<Panel_ComfirmPopup>();
        //}
        //panel_ComfirmPopup.SetMsgType(popupMsgType);
        //panel_ComfirmPopup.gameObject.SetActive(true);

        return true;
    }

    public GameObject GetPanel(WTUIState type)
    {
        WTMain main = WTMain.Instance;
        byte iType = (byte)type;
        if (panels[iType] == null)
        {
            panels[iType] = SpawnUIPanelObject(type);
        }
        //main.PlaySound("UI_Open");
        panels[iType].SetActive(true);
        panels[iType].transform.SetAsLastSibling();
        return panels[(int)type];
    }

    public void DestroyPanel(WTUIState type)
    {
        WTMain main = WTMain.Instance;
        byte iType = (byte)type;
        if (panels[iType] != null)
        {
            Destroy(panels[iType].gameObject);
            panels[iType] = null;
        }
        //main.PlaySound("UI_Open");
    }

    private GameObject SpawnUIPanelObject(WTUIState type)
    {
        GameObject go = null;
        WTUIPrefabContainer container = WTUIPrefabContainer.Instance;

        switch (type)
        {
            case WTUIState.Lobby:
                go = SpawnUIObject(container.panelLobby);
                break;
            case WTUIState.Game:
                go = SpawnUIObject(container.panelGame);
                SpawnUIObject(container.panelDay);
                break;
            case WTUIState.Shop:
                go = SpawnUIObject(container.panelShop);
                break;
            case WTUIState.SelectCharacter:
                go = SpawnUIObject(container.panelSelectCharacter);
                break;
            case WTUIState.Pause:
                go = SpawnUIObject(container.panelPause);
                break;
            case WTUIState.GameOver:
                go = SpawnUIObject(container.panelGameOver);
                break;
            case WTUIState.Story:
                go = SpawnUIObject(container.panelStory);
                break;
        }
        return go;
    }

    public bool ChangeUIState(WTUIState _state)
    {
        if (_state == uiState) return false;

        WTMain main = WTMain.Instance;
        WTUIPrefabContainer uiPrefabContainer = WTUIPrefabContainer.Instance;
        if (uiState == WTUIState.Lobby)
        {
            if (panel_Lobby != null)
            {
                Destroy(panel_Lobby.gameObject);
                panel_Lobby = null;
            }
        }
        else if (uiState == WTUIState.Game)
        {
            if(_state == WTUIState.Shop)
            {
                GetPanel(WTUIState.Shop);
                panel_Game.RT.SetAsLastSibling();
                main.player.fsm.ChangeState(main.player.fsm.IdleState);
                main.player.isPlaying = false;
                main.spawner.gameObject.SetActive(false);
            }
            else
            {
                if (panel_Game != null)
                {
                    Destroy(panel_Game.gameObject);
                    panel_Game = null;
                }
            }
        }
        uiState = _state;

        if (uiState == WTUIState.Lobby)
        {
            panel_Lobby = GetPanel(WTUIState.Lobby).GetComponent<Panel_Lobby>();

        }
        else if(uiState == WTUIState.SelectCharacter)
        {
            GetPanel(WTUIState.SelectCharacter);
        }
        else if(uiState == WTUIState.Story)
        {
            GetPanel(WTUIState.Story);
        }
        else if (uiState == WTUIState.Game)
        {
            if (panel_Game == null)
            {
                panel_Game = GetPanel(WTUIState.Game).GetComponent<Panel_Game>();
            }
        }

        return true;
    }
}
