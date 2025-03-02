
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
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
        SetAnimSpeed(fsm.enemy.stat.attack_speed);
        if(fsm.enemy.isNight)
            StartAnimation(ntName, 0, true);
        else
            StartAnimation(anName, 0, true);
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
}

