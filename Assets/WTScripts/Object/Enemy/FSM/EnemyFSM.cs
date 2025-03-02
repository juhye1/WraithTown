using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyFSM : BaseFSM
{
    #region Enemy States
    public EnemyIdleState IdleState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyDieState DieState { get; private set; }
    #endregion
    public float atkRange = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        IdleState = new EnemyIdleState(this);
        ChaseState = new EnemyChaseState(this);
        AttackState = new EnemyAttackState(this);
        DieState = new EnemyDieState(this);
        ChangeState(ChaseState);
    }

    public void IsAttackRange()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > atkRange)
        {
            ChangeState(ChaseState);
        }
        else if(isCooltime && Vector2.Distance(transform.position, player.transform.position) <= atkRange)
        {
            ChangeState(IdleState);
        }
        else if(!isCooltime && currentState == IdleState)
        {
            ChangeState(AttackState);
        }
    }

    public void WaitForCooltime()
    {
        if (!isCooltime) return;
        cooltime -= Time.deltaTime;
        if (cooltime <= 0)
        {
            isCooltime = false;
            cooltime = 1f;
        }
    }

    public override void Flip()
    {
        SetDirection(player.transform.position);
        SetFlip();
    }

    public override void SetFlip()
    {
        anim.skeleton.ScaleX = Direction == CharacterDirection.Left ? Mathf.Abs(anim.skeleton.ScaleX) : -Mathf.Abs(anim.skeleton.ScaleX);
    }
}
