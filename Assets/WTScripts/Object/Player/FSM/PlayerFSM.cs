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
                WTMain main = WTMain.Instance;
                WTGameData data = main.playerData;
                enemy.OnTakeDamaged(data.playerAb.damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // ����� ���� ����
        Gizmos.color = Color.red;

        // ���� ��ġ���� ���� ������ �������� ���� �׸�
        Vector2 center = (Vector2)transform.position + shootDir;
        Gizmos.DrawWireSphere(center, 2);
    }
   
}
