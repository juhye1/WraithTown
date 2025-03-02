using Spine;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyDieState : BaseDieState
{
    new EnemyFSM fsm;
    string ntName = "Nt_Die";
    string anName = "An_Die";
    public EnemyDieState(EnemyFSM fsm) : base(fsm)
    {
        this.fsm = fsm;
    }

    public override void Enter()
    {
        base.Enter();
        if (fsm.enemy.isNight)
        {
            TrackEntry trackEntry = fsm.anim.AnimationState.SetAnimation(0, ntName, false);
            trackEntry.Complete += (entry) => fsm.enemy.Release();
        }
        else
        {
            TrackEntry trackEntry = fsm.anim.AnimationState.SetAnimation(0, anName, false);
            trackEntry.Complete += (entry) => fsm.enemy.Release();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}