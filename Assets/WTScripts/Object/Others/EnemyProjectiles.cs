using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles : ObjectPoolBase
{
    private readonly string playerTag = "Player";
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

    public void OnShoot(BaseEnemy enemy, Vector2 dir)
    {
        SetActive(true);
        distance = enemy.stat.attack_range;
        WTMain main = WTMain.Instance;
        WTGameData data = main.playerData;
        int dmg = enemy.stat.dmg;
        if (Utils.GetRandomNum(data.playerAb.shieldChance))
        {
            dmg = (int)(enemy.stat.dmg * 0.5f);
            BasePlayer.Instance.PlayShield();
        }
        damage = dmg;
        startPos = enemy.transform.position;
        transform.position = startPos;
        rb.velocity = dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            if (collision.TryGetComponent(out BasePlayer player))
            {
                player.OnTakeDamaged(damage);
                Release();
            }
        }
    }
}