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
    }

    public override void Execute()
    {
        base.Execute();
        //if ()
        Debug.Log(fsm.moveDir);
        Debug.Log(fsm.moveSpeed);
        fsm.rb.velocity = fsm.moveDir * fsm.moveSpeed;
    }

    public override void Exit()
    {
        base.Exit();
        fsm.rb.velocity = Vector2.zero;
    }
}
