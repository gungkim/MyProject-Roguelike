using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : RecycleObject, IAttack
{
    [Header("���� ����")]
    private float enemyMaxHP;
    private float chaseSpeed;
    private float enemyDamage;
    private int enemyDefense;
    private int exp;

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
            // ����� �������� �� ���� ���
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

    public virtual void Chase()
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
        // ������ ����Ͽ� ���� ���� ���
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
        // �� ��� ó��
        Destroy(gameObject);
        Debug.Log("�� óġ");
        EnemycountUI enemycountUI = FindObjectOfType<EnemycountUI>();
        if(enemycountUI != null)
        {
            enemycountUI.CountingEnemy();
        }

        PlayerStat playerStat = FindObjectOfType<PlayerStat>();
        if (playerStat != null)
        {
            int totalExp = enemyData.exp;

            playerStat.GainExp(totalExp);
        }
    }

    protected virtual void DropLoot()
    {
        // ������ ��� ó��
        Debug.Log("����Ʈ �� ������ ���");
        // ���⼭ ������ ��� ������ �߰��ϼ���.
    }


    protected virtual int CalculateDamage(uint weaponDamage)
    {
        // ������ ���ݷ����� ���� ���
        int totalDamage = (int)weaponDamage;
        return totalDamage;
    }
}
