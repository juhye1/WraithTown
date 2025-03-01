using UnityEngine;

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
        fsm.IsAttackRange();
    }

    public override void Exit()
    {
        base.Exit();
    }
}