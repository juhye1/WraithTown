using Spine.Unity;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineEvtHandler : MonoBehaviour
{
    BasePlayer player;
    public SkeletonAnimation skeletonAnimation;

    private void Start()
    {
        skeletonAnimation.AnimationState.Complete += OnAnimationComplete;
    }

    private void OnDestroy()
    {
        skeletonAnimation.AnimationState.Complete -= OnAnimationComplete;
    }

    // �ִϸ��̼��� ������ ȣ��Ǵ� �Լ�
    private void OnAnimationComplete(TrackEntry trackEntry)
    {
        Debug.Log($"�ִϸ��̼� �����: {trackEntry.Animation.Name}");

        switch(trackEntry.Animation.Name)
        {
            case nameof(PlayerStateType.Attack):
                if(player.isPlaying && player.input.isPress)
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
    Attack,
    Die
}
