using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : RecycleObject
{
    [Header("¿˚¿« Ω∫≈»")]
    private float enemyMaxHP;
    private float chaseSpeed;
    private float enemyDamage;

    new BoxCollider2D collider;
    Rigidbody2D rigid;

    Player player;

    public EnemyData enemyData;

    protected float currentHP;

    protected virtual void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        chaseSpeed = enemyData.chaseSpeed;
        enemyMaxHP = enemyData.enemyMaxHP;
        enemyDamage = enemyData.enemyDamage;
        currentHP = enemyMaxHP;
    }

    protected virtual void Update()
    {
        Chase();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {

    }

    protected virtual void Chase()
    {
        player = GameManager.Instance.Player;

        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();

            transform.position += direction * chaseSpeed * Time.deltaTime;
        }
    }

    protected virtual void Die()
    {
        if(currentHP <= 0)
        {
            Destroy(this);
        }
    }
}
