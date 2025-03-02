using System.Xml.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyIdleState : BaseIdleState
{
    new EnemyFSM fsm;
    string ntName = "Nt_Idle";
    string anName = "An_Idle";
    public EnemyIdleState(EnemyFSM fsm) : base(fsm)
    {
        this.fsm = fsm;
    }


    public override void Enter()
    {
        base.Enter();
        if (fsm.enemy.isNight)
            StartAnimation(ntName, 0, true);
        else
            StartAnimation(anName, 0, true);
    }

    public override void Execute()
    {
        base.Execute();
        if(fsm.player.isPlaying && !fsm.player.isDead)
        {
            fsm.ChangeState(fsm.ChaseState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}