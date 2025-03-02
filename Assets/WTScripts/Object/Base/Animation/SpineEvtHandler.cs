using Spine.Unity;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineEvtHandler : MonoBehaviour
{
    public BasePlayer player;
    public SkeletonAnimation skeletonAnimation;

    private void Start()
    {
        skeletonAnimation.AnimationState.Complete += OnAnimationComplete;
        skeletonAnimation.AnimationState.Start += OnAnimationStart;
    }

    private void OnDestroy()
    {
        skeletonAnimation.AnimationState.Complete -= OnAnimationComplete;
        skeletonAnimation.AnimationState.Complete -= OnAnimationStart;
    }

    private void OnAnimationStart(TrackEntry trackEntry)
    {
        //Debug.Log($"�ִϸ��̼� ���۵�: {trackEntry.Animation.Name}");

        switch (trackEntry.Animation.Name)
        {
            case "Attack_01":
            case "Attack_02":
                player.fsm.isAttack = true;
                break;
        }
    }

    // �ִϸ��̼��� ������ ȣ��Ǵ� �Լ�
    private void OnAnimationComplete(TrackEntry trackEntry)
    {
        //Debug.Log($"�ִϸ��̼� �����: {trackEntry.Animation.Name}");

        switch(trackEntry.Animation.Name)
        {
            case "Attack_01":
            case "Attack_02":
                player.fsm.isAttack = false;
                if (player.isPlaying && player.input.isPress)
                    player.fsm.ChangeState(player.fsm.MoveState);
                else
                    player.fsm.ChangeState(player.fsm.IdleState);
                break;
        }
    }

}

public enum PlayerStateType
{
    Idle,
    Move,
    Attack_01,
    Attack_02,
    Die
}
