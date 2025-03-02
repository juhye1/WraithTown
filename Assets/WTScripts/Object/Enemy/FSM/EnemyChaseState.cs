
using System.Xml.Linq;
using UnityEngine;

public class EnemyChaseState : BaseMoveState
{
    new EnemyFSM fsm;
    string ntName = "Nt_Walk";
    string anName = "An_Walk";
    public EnemyChaseState(EnemyFSM fsm) : base(fsm)
    {
        this.fsm = fsm;
    }

    public override void Enter()
    {
        base.Enter();
        fsm.rb.velocity = (fsm.targetPos - (Vector2)fsm.transform.position).normalized * (fsm.moveSpd * fsm.enemy.stat.move_speed);
        if (fsm.enemy.isNight)
            StartAnimation(ntName, 0, true);
        else
            StartAnimation(anName, 0, true);
    }

    public override void Execute()
    {
        base.Execute();
        MoveTowardsTarget(fsm.AttackState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected void MoveTowardsTarget(IState newState)
    {
        if (fsm.rb != null && fsm.player != null)
        {
            fsm.rb.velocity = (fsm.player.transform.position - fsm.transform.position).normalized * (fsm.moveSpd * fsm.enemy.stat.move_speed);

            float distance = Vector2.Distance(fsm.transform.position, fsm.player.transform.position);
            // 타겟에게 도착하면 공격 상태로 변경
            if (distance > fsm.atkRange - 0.5f)
            {
                return;
            }
            else if (distance <= fsm.atkRange)
            {
                fsm.rb.velocity = Vector3.zero;
                fsm.ChangeState(newState);
            }                                                                         
        }
    }
}