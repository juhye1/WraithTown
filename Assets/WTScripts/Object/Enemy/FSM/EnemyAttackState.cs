
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyAttackState : BaseAttackeState
{
    new EnemyFSM fsm;
    //EnemyStatHandler stat;
    public EnemyAttackState(EnemyFSM fsm): base(fsm)
    {
        this.fsm = fsm;
        //stat = fsm.enemy.stat;
    }

    public override void Enter()
    {
        base.Enter();
        fsm.rb.velocity = Vector2.zero;
        StartAnimation(animName, 0, false);
    }

    public override void Execute()
    {
        base.Execute();
        if(!fsm.isAttack)
            fsm.IsAttackRange();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

