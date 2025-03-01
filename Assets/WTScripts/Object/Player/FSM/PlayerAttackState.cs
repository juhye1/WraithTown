using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseAttackeState
{
    private new PlayerFSM fsm;
    public float attackDelay = 0;
    public float atkspd = 1f;
    public PlayerAttackState(PlayerFSM fsm) : base(fsm)
    {
        this.fsm = fsm;
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(animName, 0, false);
        fsm.Shoot();
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {

    }
}
