using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class BaseEnemy : ObjectPoolBase, BaseObject
{
    #region ���� 
    private string projectileName;
    #endregion

    #region ���� Ŭ����
    public EnemyFSM fsm;
    #endregion

    #region ����Ƽ �Լ�
    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        if (fsm.currentState != null)
            fsm.currentState?.Execute();
        fsm.WaitForCooltime();       
    }
    #endregion

    #region �Ϲ� �޼���
    public void DeathEvt()
    {
        SetActive(false);
    }

    public override void Init()
    {
        base.Init();
    }

    public void OnAttack()
    {
        var obj = WTPoolManager.Instance.SpawnQueue<Projectiles>(projectileName);
        var dir = (fsm.targetPos - (Vector2)transform.position).normalized;
        obj.OnShoot(this, dir);
    }

    public void OnTakeDamaged()
    {
        SetActive(false);
    }

    public override void Setup()
    {
        base.Setup();
    }
    #endregion
}
