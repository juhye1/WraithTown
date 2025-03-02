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
        WTMain main = WTMain.Instance;
        WTGameData data = main.playerData;
        fsm.rb.velocity = fsm.moveDir * data.playerAb.moveSpeed;
        if (fsm.moveDir == Vector2.zero)
            fsm.ChangeState(fsm.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
        fsm.rb.velocity = Vector2.zero;
    }
}
