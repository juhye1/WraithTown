using Spine.Unity;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnySpineEvtHandler : MonoBehaviour
{
    [SerializeField]
    BaseEnemy enemy;
    public SkeletonAnimation skeletonAnimation;

    private void Start()
    {
        skeletonAnimation.AnimationState.Start += OnAnimationStart;
        skeletonAnimation.AnimationState.Complete += OnAnimationComplete;
    }

    private void OnDestroy()
    {
        skeletonAnimation.AnimationState.Start -= OnAnimationStart;
        skeletonAnimation.AnimationState.Complete -= OnAnimationComplete;
    }

    private void OnAnimationStart(TrackEntry trackEntry)
    {
        Debug.Log($"애니메이션 시작됨: {trackEntry.Animation.Name}");

        switch (trackEntry.Animation.Name)
        {
            case "Attack_01":
                enemy.fsm.isAttack = true;
                break;
        }
    }

    // 애니메이션이 끝나면 호출되는 함수
    private void OnAnimationComplete(TrackEntry trackEntry)
    {
        Debug.Log($"애니메이션 종료됨: {trackEntry.Animation.Name}");

        switch (trackEntry.Animation.Name)
        {
            case "Attack_01":
                enemy.fsm.isAttack = false;
                enemy.fsm.isCooltime = true;
                enemy.fsm.ChangeState(enemy.fsm.IdleState);
                break;
        }
    }
}
