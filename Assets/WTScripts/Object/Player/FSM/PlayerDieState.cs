using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : BaseDieState
{
    private new PlayerFSM fsm;
    public PlayerDieState(PlayerFSM fsm) : base(fsm)
    {
        this.fsm = fsm;
    }

    public override void Enter()
    {
        if (fsm.player.isDead) return;
        base.Enter();
        StartAnimation(animName);
    }

    public override void Exit()
    {
        base.Exit();
        fsm.player.isDead = false;
    }
}
