using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CMUIState
{
    None = -1,
    Ready = 0,
    Lobby,
    Loading,
    Game,
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

    //[NonSerialized] public Panel_Lobby panel_Lobby;
    //[NonSerialized] public Panel_Loading panel_Loading;
    //[NonSerialized] public Panel_Game panel_Game;

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

}
