using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDieState : BaseState
{
    public BaseDieState(BaseFSM fsm) : base(fsm)
    {
        animName = "die";
    }

    public override void Enter()
    {
        fsm.rb.velocity = Vector3.zero;
    }

    public override void Execute()
    {
       
    }

    public override void Exit()
    {
      
    }
}
