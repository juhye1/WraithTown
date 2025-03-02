using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFSM : BaseFSM
{
    public Vector2 moveDir;
    public Vector2 lookDir;
    public Vector2 shootDir;
    private LayerMask enemyMask;
    public string projectileName;

    #region Player States
    public PlayerMoveState MoveState { get; private set; }
    public PlayerDieState DieState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    
    #endregion
    protected override void Awake()
    {
        base.Awake();
        enemyMask = LayerMask.GetMask("Enemy");
    }
    public void Start()
    {
        moveSpd *= WTMain.Instance.playerData.playerAb.moveSpeed;
    }


    public void Init()
    {
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
        shootDir = dir - (Vector2)transform.position;
    }

    public void Shoot()
    {
        if (player.skinType == PlayerSkin.Miho)
        {
            var obj = WTPoolManager.Instance.SpawnQueue<Projectiles>(projectileName);
            obj.OnShoot(player, shootDir.normalized);
        }
        else if(player.skinType == PlayerSkin.Kebi)
        {
            Invoke("Slash", 5 / 30f);
        }
    }

    private void Slash()
    {
        var colls = Physics2D.OverlapCircleAll((Vector2)transform.position + shootDir.normalized, WTMain.Instance.playerData.playerAb.attackRange, enemyMask);
        foreach(var coll in colls) 
        { 
            if(coll.TryGetComponent(out BaseEnemy enemy))
            {
                enemy.OnTakeDamaged(player.stat.status.dmg);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // 기즈모 색상 설정
        Gizmos.color = Color.red;

        // 현재 위치에서 슈팅 방향을 기준으로 원을 그림
        Vector2 center = (Vector2)transform.position + shootDir;
        Gizmos.DrawWireSphere(center, 2);
    }
   
}
