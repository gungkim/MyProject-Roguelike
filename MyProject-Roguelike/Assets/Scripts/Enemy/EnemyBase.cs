using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : RecycleObject, IAttack
{
    [Header("적의 스탯")]
    private float enemyMaxHP;
    private float chaseSpeed;
    private float enemyDamage;
    private int enemyDefense;

    new BoxCollider2D collider;
    Rigidbody2D rigid;

    Player player;

    public EnemyData enemyData;

    protected float currentHP;
    public uint AttackPower => (uint)enemyDamage;

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
        enemyDefense = enemyData.enemyDefense;
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
        if (collision.CompareTag("Weapon"))
        {
            // 무기와 접촉했을 때 피해 계산
            WeaponBase weapon = collision.GetComponent<WeaponBase>();
            if (weapon != null)
            {
                int damageDealt = CalculateDamage(weapon.AttackPower);
                Damaged(damageDealt);
            }
        }
    }

    protected virtual void Attack()
    {
        player = GameManager.Instance.Player;
        PlayerStat playerStat = player.PlayerStat;
        float totalDamage = enemyDamage - playerStat.PlayerAttackPower;
        // playerStat.currentHP -= totalDamage;
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

    protected virtual void Damaged(int damage)
    {
        // 방어력을 고려하여 실제 피해 계산
        int finalDamage = Mathf.Max(damage - (int)enemyDefense, 0);
        Debug.Log($"{finalDamage}");
        currentHP -= finalDamage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // 적 사망 처리
        Destroy(gameObject);
        Debug.Log("적 처치");
    }


    protected virtual int CalculateDamage(uint weaponDamage)
    {
        // 무기의 공격력으로 피해 계산
        int totalDamage = (int)weaponDamage;
        return totalDamage;
    }
}
