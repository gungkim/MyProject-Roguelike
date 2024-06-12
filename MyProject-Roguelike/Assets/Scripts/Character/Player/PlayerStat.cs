using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [Header("캐릭터의 스탯")]
    private float attackPower;        
    private int defense;              
    private float maxHP;   
    private float criticalChance;     
    private float attackRange;          
    private float moveSpeed;
    public float coolTime;


    private ItemData_Accessory itemData_Accessory;
    private CharacterData characterData;

    // 이벤트 정의
    public event Action OnStatsChanged;

    public ItemData_Accessory ItemData_Accessory
    {
        get { return itemData_Accessory; }
        set
        {
            itemData_Accessory = value;
            RecalculateStats();
        }
    }

    public CharacterData Character
    {
        get { return characterData; }
        set
        {
            characterData = value;
            RecalculateStats();
        }
    }

    private void RecalculateStats()
    {
        if (characterData == null)
        {
            Debug.LogError("CharacterData is not assigned!");
            return;
        }

        attackPower = characterData.attackPower;
        defense = characterData.defense;
        maxHP = characterData.maxHP;
        criticalChance = characterData.criticalChance;
        attackRange = characterData.attackRange;
        moveSpeed = characterData.moveSpeed;

        if (itemData_Accessory != null)
        {
            attackPower += itemData_Accessory.damage;
            defense += itemData_Accessory.defense;
            maxHP += itemData_Accessory.maxHP;
            attackRange += itemData_Accessory.attackRange;
            moveSpeed += itemData_Accessory.moveSpeed;
            criticalChance += itemData_Accessory.criticalChance;
        }

        // 스탯이 변경되었음을 알리기 위해 이벤트 호출
        OnStatsChanged?.Invoke();
    }

    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float MaxHP { get { return maxHP; } set { maxHP = value; } }
    public float AttackPower { get { return attackPower; } set { attackPower = value; } }
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public float CriticalChance { get { return criticalChance; } set { criticalChance = value; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IAttack attack = collision.GetComponent<IAttack>();
        Damaged(attack.AttackPower);
    }

    private void Damaged(float damage)
    {
        float finalDamage = damage * ((100 - defense) * 0.01f);
        maxHP -= finalDamage;
        if (maxHP <= 0)
        {
            maxHP = 0;
            // 플레이어가 사망했음을 알리는 로직 추가
        }
    }


}

// 스탯은 추후에 캐릭터 생성 후 캐릭터로 옮길 것.
// 캐릭터에서 스탯을 받아서 기본 스탯과 합산하여 계산.
// UI생성 후 성장치 추가. 성장치에서도 스탯 합산하기