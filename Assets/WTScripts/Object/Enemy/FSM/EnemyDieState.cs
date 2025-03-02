using UnityEngine;

public class EnemyDieState : BaseDieState
{
    new EnemyFSM fsm;
    public EnemyDieState(EnemyFSM fsm) : base(fsm)
    {
        this.fsm = fsm;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
}