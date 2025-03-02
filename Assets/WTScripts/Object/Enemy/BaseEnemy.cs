using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class BaseEnemy : ObjectPoolBase, BaseObject
{
    #region ���� 
    private string projectileName = "EnemyProjectiles";
    public bool isDead;
    public bool isNight;
    private WTEnemyType skinType;
    [SerializeField]
    public SkeletonDataAsset[] newSkeletonData;
    [SerializeField]
    string damageEffect = "DamageEffect";
    [SerializeField]
    string killEffect = "KillEffect";
    [SerializeField]
    string goldGoods = "Goods";
    #endregion

    #region ���� Ŭ����
    public EnemyFSM fsm;
    public EnemyStatHandler stat;
    #endregion
    void Start()
    {
        fsm.Init();
    }

    #region ����Ƽ �Լ�

    protected virtual void Update()
    {
        if (fsm.currentState != null)
            fsm.currentState?.Execute(); 
        if(!fsm.player.isPlaying || fsm.player.isDead)
        {
            Release();
        }
    }
    #endregion

    #region �Ϲ� �޼���
    public void DeathEvt()
    {
        isDead = true;
        DropGoods();
        fsm.ChangeState(fsm.DieState);
    }
    private void DropGoods()
    {
        if(stat.template.dead_drop_coin != 0)
        {
            float rand = Random.Range(0, 1);
            if (rand <= stat.template.drop_weight)
            {
                Debug.Log("����" + rand);
                var obj = WTPoolManager.Instance.SpawnQueue<DropGoods>(goldGoods);
                obj.Setup(transform.position);
                WTGlobal.CallEventDelegate(WTEventType.ChangeGold, stat.template.dead_drop_coin);
            }
        }

        if(stat.template.dead_drop_soul != 0)
        {
            float rand = Random.Range(0, 1);
            Debug.Log("�ҿ�" + rand);
            if (rand <= stat.template.drop_weight)
                WTGlobal.CallEventDelegate(WTEventType.ChangePoint, stat.template.dead_drop_soul);
        }
    }

    public override void Init()
    {
        base.Init();
    }

    public void OnAttack()
    {
        if(stat.template.attack_range == 1)
        {
            WTMain main = WTMain.Instance;
            WTGameData data = main.playerData;
            int dmg = stat.dmg;
            if (Utils.GetRandomNum(data.playerAb.shieldChance))
            {
                dmg = (int)(stat.dmg * 0.5f);
                BasePlayer.Instance.PlayShield();
            }
            BasePlayer.Instance.OnTakeDamaged(dmg);
        }
        else
        {
            var obj = WTPoolManager.Instance.SpawnQueue<EnemyProjectiles>(projectileName);
            var dir = (BasePlayer.Instance.transform.position - transform.position).normalized;
            obj.OnShoot(this, dir);
        }
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
                isNight = true;
                break;
            case WTEnemyType.Haetae:
                idx = 1;
                isNight = true;
                break;
            case WTEnemyType.Exorcist:
                idx = 2;
                isNight = true;
                break;
            case WTEnemyType.Student:
                idx = 3;
                isNight = false;
                break;
            case WTEnemyType.Maniac:
                idx = 4;
                isNight = false;
                break;
            case WTEnemyType.Tourist:
                idx = 5;
                isNight = false;
                break;
        }
        fsm.anim.skeletonDataAsset = newSkeletonData[idx];
        fsm.anim.Initialize(true);
        fsm.ChangeState(fsm.ChaseState);
    }

    public void Setup(WTEnemyUnitStatsTemplate template)
    {
        Setup();
        stat.Init(template);
        SetSkin((WTEnemyType)template.enemyunit_id);
    }
    #endregion
}
