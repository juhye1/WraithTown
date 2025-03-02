using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseIdleState : BaseState
{
    public BaseIdleState(BaseFSM fsm): base(fsm)
    {
        animName = "Idle";
    }

    public override void Enter()
    {
        fsm.rb.velocity = Vector2.zero;
        StartAnimation(animName);
    }

    public override void Execute()
    {
        BasePlayer player = BasePlayer.Instance;
        if(player.isPlaying)
        {
            fsm.Flip();
        }
    }

    public override void Exit()
    {

    }
}
