using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttackeState : BaseState
{
    public BaseAttackeState(BaseFSM fsm) : base(fsm)
    {
        animName = "Attack_01";
    }

    public override void Enter()
    {
        
    }

    public override void Execute()
    {
        fsm.Flip();
    }

    public override void Exit()
    {
        
    }
}
