using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectiles : ObjectPoolBase
{
    private readonly string enemyTag = "Enemy";
    [SerializeField]
    SpriteRenderer model;
    [SerializeField]
    private Vector2 startPos = new();
    [SerializeField]
    private Rigidbody2D rb;
    private float speed = 5f;
    private float distance = 10f;
    private int damage;
    private void Update()
    {
        if(!BasePlayer.Instance.isPlaying || BasePlayer.Instance.isDead)
            Release();
        if (Vector2.Distance(startPos, transform.position) <= distance) return;
        Release();
    }

    public override void Init()
    {
        base.Init();
    }

    public override void Setup()
    {
        base.Setup();
    }

    public override void Release()
    {
        rb.velocity = Vector2.zero;
        base.Release();
    }

    public void OnShoot(BasePlayer player, Vector2 dir)
    {
        SetActive(true);
        WTMain main = WTMain.Instance;
        WTGameData data = main.playerData;
        distance = data.playerAb.attackRange;
        int dmg = data.playerAb.damage;
        if (Utils.GetRandomNum(data.playerAb.criticalChance))
        {
            dmg = dmg * 2;
            BasePlayer.Instance.PlayCritical();
            int idx = Random.Range(1, 5);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.bgmClip["RevolverShot_0" + idx]);
        }
        else
        {
            int idx = Random.Range(1, 6);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.bgmClip["MachineGunShot_0" + idx]);
        }

        damage = dmg;
        startPos = player.projectileTr.position;
        transform.position = startPos;
        rb.velocity = dir.normalized * speed;
    }

    public void OnShoot(BaseEnemy enemy , Vector2 dir)
    {
        SetActive(true);
        distance = enemy.stat.template.attack_range;
        damage = enemy.stat.template.dmg;
        startPos = enemy.transform.position;
        transform.position = startPos;
        rb.velocity = dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(enemyTag))
        {
            if(collision.TryGetComponent(out BaseEnemy enemy))
            {
                enemy.OnTakeDamaged(damage);
                Release();
            }
        }
    }
}
