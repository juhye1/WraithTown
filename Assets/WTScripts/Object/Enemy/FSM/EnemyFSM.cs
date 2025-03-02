using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyFSM : BaseFSM
{
    public BaseEnemy enemy;
    #region Enemy States
    public EnemyIdleState IdleState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyDieState DieState { get; private set; }
    #endregion
    public float atkRange => enemy.stat.template.attack_range;
    // Start is called before the first frame update

    public void Init()
    {
        IdleState = new EnemyIdleState(this);
        ChaseState = new EnemyChaseState(this);
        AttackState = new EnemyAttackState(this);
        DieState = new EnemyDieState(this);
        if(enemy == null)
            enemy = GetComponent<BaseEnemy>();
        ChangeState(ChaseState);
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
