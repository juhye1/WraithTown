using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFSM : BaseFSM
{
    public float moveSpeed;
    public Vector2 moveDir;
    public Vector2 shootDir;

    public string projectileName;
    #region Player States
    public PlayerMoveState MoveState { get; private set; }
    public PlayerDieState DieState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerIdleState IdleState { get; private set; }

    #endregion

    public void Init()
    {
        player = BasePlayer.Instance;
        MoveState = new PlayerMoveState(this);
        IdleState = new PlayerIdleState(this);
        DieState = new PlayerDieState(this);
        AttackState = new PlayerAttackState(this);

        ChangeState(IdleState);
    }

    public void Move(Vector2 dir)
    {
        //if(currentState == )
        moveDir = dir;
    }

    public void Aiming(Vector2 dir)
    {
        shootDir = dir;
    }

    public void Shoot()
    {
        var obj = WTPoolManager.Instance.SpawnQueue<Projectiles>(projectileName);
        obj.OnShoot(player ,shootDir);
    }

    public void WaitForCooltime()
    {
        if (!isCooltime) return;
        cooltime -= Time.deltaTime;
        if (cooltime <= 0)
        {
            isAttack = false;
            isCooltime = false;
            cooltime = 1f;
        }
    }
}
