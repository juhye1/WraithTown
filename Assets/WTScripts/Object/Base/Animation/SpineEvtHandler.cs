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
        Debug.Log($"애니메이션 시작됨: {trackEntry.Animation.Name}");

        switch (trackEntry.Animation.Name)
        {
            case nameof(PlayerStateType.Attack_01):
                player.fsm.isAttack = true;
                break;
        }
    }

    // 애니메이션이 끝나면 호출되는 함수
    private void OnAnimationComplete(TrackEntry trackEntry)
    {
        Debug.Log($"애니메이션 종료됨: {trackEntry.Animation.Name}");

        switch(trackEntry.Animation.Name)
        {
            case nameof(PlayerStateType.Attack_01):
                player.fsm.isCooltime = true;
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
