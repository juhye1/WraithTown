using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDieState : BaseState
{
    public BaseDieState(BaseFSM fsm) : base(fsm)
    {
        animName = "Death";
    }

    public override void Enter()
    {
        StartAnimation(animName);
        fsm.rb.velocity = Vector3.zero;
    }

    public override void Execute()
    {
       
    }

    public override void Exit()
    {
      
    }
}
