
using Spine;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyAttackState : BaseAttackeState
{
    new EnemyFSM fsm;
    //EnemyStatHandler stat;

    string ntName = "Nt_Attack";
    string anName = "An_Attack";

    public EnemyAttackState(EnemyFSM fsm): base(fsm)
    {
        this.fsm = fsm;
        //stat = fsm.enemy.stat;
    }

    public override void Enter()
    {
        base.Enter();
        fsm.rb.velocity = Vector2.zero;
 
        if (fsm.enemy.isNight)
        {
            TrackEntry trackEntry = fsm.anim.AnimationState.SetAnimation(0, ntName, false);
            trackEntry.Start += OnAnimationStart;
            trackEntry.Complete += OnAnimationComplete;
        }
        else
        {
            TrackEntry trackEntry = fsm.anim.AnimationState.SetAnimation(0, anName, false);
            trackEntry.Start += OnAnimationStart;
            trackEntry.Complete += OnAnimationComplete;
        }
        SetAnimSpeed(fsm.enemy.stat.template.attack_speed);
        fsm.enemy.OnAttack();
    }

    public override void Execute()
    {
        base.Execute();
        if(!fsm.isAttack)
        {
            if (Vector2.Distance(fsm.transform.position, fsm.player.transform.position) > fsm.atkRange)
            {
                fsm.ChangeState(fsm.ChaseState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        SetAnimSpeed(1);
    }

    private void OnAnimationStart(TrackEntry trackEntry)
    {
        fsm.isAttack = true;
    }

    private void OnAnimationComplete(TrackEntry trackEntry)
    {
        fsm.isAttack = false;
        float distance = Vector2.Distance(fsm.transform.position, fsm.player.transform.position);
        // 타겟에게 도착하면 공격 상태로 변경
        if (distance > fsm.atkRange)
        {
            fsm.ChangeState(fsm.ChaseState);
        }
        else if (distance <= fsm.atkRange)
        {
            fsm.ChangeState(fsm.AttackState);
        }
    }
}

