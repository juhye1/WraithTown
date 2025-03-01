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
    private float distance = 5f;
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

    public void OnShoot(BasePlayer player, Vector2 dir)
    {
        startPos = player.projectileTr.position;
        transform.position = startPos;
        rb.velocity = dir * speed;
    }

    public void OnShoot(BaseEnemy enemy , Vector2 dir)
    {
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
                enemy.OnTakeDamaged();
                Release();
            }
        }
    }
}
