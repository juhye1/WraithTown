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
    public Image selectImageMiho;
    public Image selectImageKebi;

    private void OnEnable()
    {
        selectCharacterID = WTConstants.UnitIDMiho;
        kebiSkel.AnimationState.SetAnimation(0, PlayerStateType.Idle.ToString(), true);
        mihoSkel.AnimationState.SetAnimation(0, PlayerStateType.Attack_01.ToString(), false).Complete += AnimationCompleteEventMiho;
    }

    public void OnClickBtn_Miho()
    {
        selectCharacterID = WTConstants.UnitIDMiho;
        selectImageMiho.enabled = true;
        selectImageKebi.enabled = false;
        kebiSkel.AnimationState.SetAnimation(0, PlayerStateType.Idle.ToString(), true);
        mihoSkel.AnimationState.SetAnimation(0, PlayerStateType.Attack_01.ToString(), false).Complete += AnimationCompleteEventMiho;
    }

    public void OnClickBtn_Kebi()
    {
        selectCharacterID = WTConstants.UnitIDKebi;
        selectImageMiho.enabled = false;
        selectImageKebi.enabled = true;
        mihoSkel.AnimationState.SetAnimation(0, PlayerStateType.Idle.ToString(), true);
        kebiSkel.AnimationState.SetAnimation(0, PlayerStateType.Attack_01.ToString(), false).Complete += AnimationCompleteEventKebi;
    }

    private void AnimationCompleteEventKebi(TrackEntry de)
    {
        kebiSkel.AnimationState.SetAnimation(0, PlayerStateType.Idle.ToString(), true);
    }
    private void AnimationCompleteEventMiho(TrackEntry de)
    {
        mihoSkel.AnimationState.SetAnimation(0, PlayerStateType.Idle.ToString(), true);
    }

    public void OnClickBtn_Start()
    {
        SpawnCharacter(selectCharacterID);
    }

    private void SpawnCharacter(ushort id)
    {
        WTMain main = WTMain.Instance;
        main.SpawnPlayerCharacter(id);
        WTUIMain uiMain = WTUIMain.Instance;
        uiMain.DestroyPanel(WTUIState.SelectCharacter);
    }
}
