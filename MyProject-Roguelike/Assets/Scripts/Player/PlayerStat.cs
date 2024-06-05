using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [Header("�÷��̾��� �⺻ ����")]
    public float BaseAttackPower;        
    public float BaseDefense;              
    public float BaseAttackSpeed;        
    public float BaseMaxHp = 100.0f;   
    public float BaseCriticalChance;     
    public float BaseAttackRange;          
    public float BaseMoveSpeed;
    public float BasecoolTime = 5.0f;

    [Header("�÷��̾ ����ްԵǴ� ����")]
    private float attackPower;        
    private float Defense;              
    private float maxHp = 100.0f;   
    private float criticalChance;     
    private float attackRange;          
    private float moveSpeed;
    public float coolTime = 5.0f;


    void Start()
    {
        
    }

    public float Speed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float MaxHp { get { return maxHp; } set { maxHp = value; } }
    public float AttackPower { get { return attackPower; } set { attackPower = value; } }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IAttack attack = collision.GetComponent<IAttack>();
        Damaged(attack.AttackPower);
    }

    private void Damaged(float damage)
    {

    }
}

// ������ ���Ŀ� ĳ���� ���� �� ĳ���ͷ� �ű� ��.