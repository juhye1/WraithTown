using System.Xml.Linq;
using UnityEngine;

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
            StartAnimation(ntName, 0, true);
        else
            StartAnimation(anName, 0, true);
    }

    public override void Exit()
    {
        base.Exit();
    }
}