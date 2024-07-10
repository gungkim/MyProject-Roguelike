using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Elite : EnemyBase
{
    private float extraExp;
    private float dropChance;

    public EnemyData_Elite enemyData_Elite;

    protected override void Start()
    {
        base.Start();
        if (enemyData_Elite != null)
        {
            extraExp = enemyData_Elite.extraExp;
            dropChance = enemyData_Elite.dropChance;
        }
    }

    protected override void Die()
    {
        Destroy(gameObject);
        Debug.Log("����Ʈ �� óġ");
        EnemycountUI enemycountUI = FindObjectOfType<EnemycountUI>();
        if (enemycountUI != null)
        {
            enemycountUI.CountingEnemy();
        }

        PlayerStat playerStat = FindObjectOfType<PlayerStat>();
        if (playerStat != null)
        {
            int totalExp = enemyData.exp + (int)extraExp;
            playerStat.GainExp(totalExp);
            if (UnityEngine.Random.value <= dropChance)
            {
                DropLoot();
            }
        }
    }

    protected override void DropLoot()
    {
        // ����Ʈ ������ ������ ��� ó��
        Debug.Log("����Ʈ �� ������ ���");
        // ���⼭ ������ ��� ������ �߰��ϼ���.
    }
}
