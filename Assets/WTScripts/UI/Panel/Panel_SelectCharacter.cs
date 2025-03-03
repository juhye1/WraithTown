using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SelectCharacter : MonoBehaviour
{
    private ushort selectCharacterID;
    public SkeletonGraphic mihoSkel;
    public SkeletonGraphic kebiSkel;    
    public SkeletonGraphic mihoSkel_G;
    public SkeletonGraphic kebiSkel_G;
    public Image selectImageMiho;
    public Image selectImageKebi;
    public Color disableColor;

    private bool isSpawn = false;

    private void OnEnable()
    {
        selectCharacterID = WTConstants.UnitIDMiho;
        //kebiSkel.AnimationState.SetAnimation(0, PlayerStateType.Idle.ToString(), true);
        //mihoSkel.AnimationState.SetAnimation(0, PlayerStateType.Attack_01.ToString(), false).Complete += AnimationCompleteEventMiho;
    }

    public void OnClickBtn_Miho()
    {
        if (isSpawn) return;
        isSpawn = true;
        selectCharacterID = WTConstants.UnitIDMiho;
        //selectImageMiho.enabled = true;
        //selectImageKebi.enabled = false;
        //kebiSkel.enabled = false;
        kebiSkel.color = disableColor;
        //kebiSkel_G.enabled = true;
       // kebiSkel.Skeleton.SetColor(color);
        //kebiSkel.AnimationState.SetAnimation(0, PlayerStateType.Idle.ToString(), true);
        mihoSkel.AnimationState.SetAnimation(0, PlayerStateType.Attack_01.ToString(), false).Complete += AnimationCompleteEventMiho;
    }

    public void OnClickBtn_Kebi()
    {
        if (isSpawn) return;
        isSpawn = true;
        selectCharacterID = WTConstants.UnitIDKebi;
        //selectImageMiho.enabled = false;
        //selectImageKebi.enabled = true;
        mihoSkel.color = disableColor;
        //mihoSkel.enabled = false;
        //mihoSkel_G.enabled = true;
        //mihoSkel.AnimationState.SetAnimation(0, PlayerStateType.Idle.ToString(), true);
        kebiSkel.AnimationState.SetAnimation(0, PlayerStateType.Attack_01.ToString(), false).Complete += AnimationCompleteEventKebi;
    }

    private void AnimationCompleteEventKebi(TrackEntry de)
    {
        kebiSkel.AnimationState.SetAnimation(0, PlayerStateType.Idle.ToString(), true);
        Invoke("SpawnCharacterDelay", 0.5f);
    }
    private void AnimationCompleteEventMiho(TrackEntry de)
    {
        mihoSkel.AnimationState.SetAnimation(0, PlayerStateType.Idle.ToString(), true);
        isSpawn = true;
        Invoke("SpawnCharacterDelay", 0.5f);
    }

    private void SpawnCharacterDelay()
    {
        SpawnCharacter(selectCharacterID);
    }

    public void OnClickBtn_Start()
    {
        //SpawnCharacter(selectCharacterID);
    }

    private void SpawnCharacter(ushort id)
    {
        WTMain main = WTMain.Instance;
        main.SpawnPlayerCharacter(id);
        WTUIMain uiMain = WTUIMain.Instance;
        uiMain.DestroyPanel(WTUIState.SelectCharacter);
    }
}
