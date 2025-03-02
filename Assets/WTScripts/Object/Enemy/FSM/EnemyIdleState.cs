using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyIdleState : BaseIdleState
{
    new EnemyFSM fsm;
    public EnemyIdleState(EnemyFSM fsm) : base(fsm)
    {
        this.fsm = fsm;
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
        if(fsm.player.isPlaying && !fsm.player.isDead)
        {
            fsm.ChangeState(fsm.ChaseState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}