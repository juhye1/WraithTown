using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class BaseEnemy : ObjectPoolBase, BaseObject
{
    #region 변수 
    private string projectileName;
    public bool isDead;
    public bool isNight;
    private WTEnemyType skinType;
    [SerializeField]
    public SkeletonDataAsset[] newSkeletonData;
    [SerializeField]
    string damageEffect = "DamageEffect";
    [SerializeField]
    string killEffect = "KillEffect";
    #endregion

    #region 하위 클래스
    public EnemyFSM fsm;
    public WTEnemyUnitStatsTemplate stat;
    #endregion

    #region 유니티 함수
    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        if (fsm.currentState != null)
            fsm.currentState?.Execute();   
    }
    #endregion

    #region 일반 메서드
    public void DeathEvt()
    {
        isDead = true;
        DropGoods();
        fsm.ChangeState(fsm.DieState);
    }
    private void DropGoods()
    {
        if(stat.dead_drop_coin != 0)
        {
            float rand = Random.Range(0, 1);
            Debug.Log("코인" + rand);
            if (rand <= stat.drop_weight)
                WTGlobal.CallEventDelegate(WTEventType.ChangeGold, stat.dead_drop_coin);
        }

        if(stat.dead_drop_soul != 0)
        {
            float rand = Random.Range(0, 1);
            Debug.Log("소울" + rand);
            if (rand <= stat.drop_weight)
                WTGlobal.CallEventDelegate(WTEventType.ChangePoint, stat.dead_drop_soul);
        }
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

    public void OnTakeDamaged(int damage)
    {
        if (isDead) return;
        stat.hp -= damage;
        
        if (stat.hp <= 0)
        {
            var effect = WTPoolManager.Instance.SpawnQueue<KillEffect>(killEffect);
            effect.Setup(gameObject);
            DeathEvt();
        }
        else
        {
            var effect = WTPoolManager.Instance.SpawnQueue<DamageEffect>(damageEffect);
            effect.Setup(gameObject);
        }
    }

    public override void Setup()
    {
        base.Setup();
        isDead = false;
    }

    public void SetSkin(WTEnemyType type)
    {
        skinType = type;
        int idx = 0;
        switch(skinType)
        {
            case WTEnemyType.Hound:
                idx = 0;
                break;
            case WTEnemyType.Haetae:
                idx = 1;
                break;
            case WTEnemyType.Exorcist:
                idx = 2;
                break;
            case WTEnemyType.Student:
                idx = 3;
                break;
            case WTEnemyType.Maniac:
                idx = 4;
                break;
            case WTEnemyType.Tourist:
                idx = 5;
                break;
        }
        fsm.anim.skeletonDataAsset = newSkeletonData[idx];
        fsm.anim.Initialize(true);
        fsm.ChangeState(fsm.ChaseState);
    }

    public void Setup(WTEnemyUnitStatsTemplate template, bool _isNight)
    {
        Setup();
        stat = template;
        SetSkin((WTEnemyType)template.enemyunit_id);
        isNight = _isNight;
    }
    #endregion
}
