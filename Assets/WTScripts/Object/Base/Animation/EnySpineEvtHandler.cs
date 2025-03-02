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

    private void Awake()
    {
        skeletonAnimation.AnimationState.Start -= OnAnimationStart;
        skeletonAnimation.AnimationState.Complete -= OnAnimationComplete;
        skeletonAnimation.AnimationState.Start += OnAnimationStart;
        skeletonAnimation.AnimationState.Complete += OnAnimationComplete;
    }

    private void OnAnimationStart(TrackEntry trackEntry)
    {
        //Debug.Log($"�ִϸ��̼� ���۵�: {trackEntry.Animation.Name}");

        switch (trackEntry.Animation.Name)
        {
            case "Nt_Attack":
            case "An_Attack":
                enemy.fsm.isAttack = true;
                enemy.OnAttack();
                break;
        }
    }

    // �ִϸ��̼��� ������ ȣ��Ǵ� �Լ�
    private void OnAnimationComplete(TrackEntry trackEntry)
    {
        //Debug.Log($"�ִϸ��̼� �����: {trackEntry.Animation.Name}");
        Debug.LogWarning(trackEntry.Animation.Name);
        switch (trackEntry.Animation.Name)
        {
            case "Nt_Attack":
            case "An_Attack":
                enemy.fsm.isAttack = false;
                break;
            case "Nt_Die":
            case "An_Die":
                enemy.isDead = false;
                break;
        }
    }
}
