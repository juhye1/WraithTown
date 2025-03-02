using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : BaseMoveState
{
    private new PlayerFSM fsm;
    
    public PlayerMoveState(PlayerFSM fsm) : base(fsm)
    {
        this.fsm = fsm; 
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(animName);
    }

    public override void Execute()
    {
        base.Execute();
        //if ()
        fsm.rb.velocity = fsm.moveDir * fsm.moveSpd;
        if(fsm.moveDir == Vector2.zero)
            fsm.ChangeState(fsm.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
        fsm.rb.velocity = Vector2.zero;
    }
}
