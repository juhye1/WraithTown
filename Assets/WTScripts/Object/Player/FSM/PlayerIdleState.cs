using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class PlayerIdleState : BaseIdleState
{
    private new PlayerFSM fsm;

    public PlayerIdleState(PlayerFSM fsm): base(fsm)
    {
        this.fsm = fsm;
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(animName);
        fsm.rb.velocity = Vector2.zero;
    }

    public override void Execute()
    {
        base.Execute();

    }

    public override void Exit()
    {
        base.Exit();
    }
}
