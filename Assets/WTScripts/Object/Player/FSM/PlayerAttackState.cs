using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseAttackeState
{
    private new PlayerFSM fsm;
    public float attackDelay = 0;
    public float atkspd = 1f;
    public bool isFirst;
    public string animName2 = "Attack_02";
    public PlayerAttackState(PlayerFSM fsm) : base(fsm)
    {
        this.fsm = fsm;
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimSpeed(1 + (1 - WTMain.Instance.playerData.playerAb.attackSpeed));
        if (isFirst)
        {
            isFirst = false;
            StartAnimation(animName, 0, false);
        }
        else
        {
            isFirst = true;
            StartAnimation(animName2, 0, false);
        }
        
        fsm.Shoot();
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        SetAnimSpeed(1);
    }
}
