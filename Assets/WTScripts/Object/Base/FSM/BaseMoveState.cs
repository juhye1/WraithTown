using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMoveState : BaseState
{
    
    public BaseMoveState(BaseFSM fsm) : base(fsm)
    {
        animName = "Walk";
        fsm.Flip();
    }

    public override void Enter()
    {
        StartAnimation(animName);
    }

    public override void Execute()
    {
        fsm.Flip();
    }

    public override void Exit()
    {

    }

}
