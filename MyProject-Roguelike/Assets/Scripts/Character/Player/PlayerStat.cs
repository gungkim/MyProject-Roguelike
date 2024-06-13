using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStat : MonoBehaviour, IAttack
{
    [Header("캐릭터의 스탯")]
    private int playerAttackPower;
    private int defense;
    private float maxHP;
    private float criticalChance;
    private float attackRange;
    private float attackSpeed;
    private float moveSpeed;
    public float coolTime;
    public int expGain;

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

        playerAttackPower = characterData.playerAttackPower;
        defense = characterData.defense;
        maxHP = characterData.maxHP;
        criticalChance = characterData.criticalChance;
        attackRange = characterData.attackRange;
        attackSpeed = characterData.attackSpeed;
        moveSpeed = characterData.moveSpeed;
        coolTime = characterData.coolDown;
        expGain = characterData.expGain;

        if (itemData_Accessory != null)
        {
            playerAttackPower += itemData_Accessory.damage;
            defense += itemData_Accessory.defense;
            maxHP += itemData_Accessory.maxHP;
            attackRange += itemData_Accessory.attackRange;
            moveSpeed += itemData_Accessory.moveSpeed;
            attackSpeed += itemData_Accessory.attackSpeed;
            criticalChance += itemData_Accessory.criticalChance;
            coolTime += itemData_Accessory.coolDown;
            expGain += itemData_Accessory.expGain;
        }

        // 스탯이 변경되었음을 알리기 위해 이벤트 호출
        OnStatsChanged?.Invoke();
    }

    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float MaxHP { get { return maxHP; } set { maxHP = value; } }
    public int PlayerAttackPower { get { return playerAttackPower; } set { playerAttackPower = value; } }
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public float CriticalChance { get { return criticalChance; } set { criticalChance = value; } }

    public float CoolTime { get { return coolTime; } set { coolTime = value; } }

    public float AttackSpeed { get { return attackSpeed; }  set { attackSpeed = value; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IAttack attack = collision.GetComponent<IAttack>();
        Damaged(attack.AttackPower);
    }
    public uint AttackPower { get; }

    private void Damaged(float damage)
    {
        float finalDamage = damage * ((100 - defense) * 0.01f);
        maxHP -= finalDamage;
        if (maxHP <= 0)
        {
            maxHP = 0;
            // 플레이어가 사망했음을 알리는 로직 추가
            Player player = GameManager.Instance.Player;
            player.PlayerDie();
        }
    }
}

// UI생성 후 성장치 추가. 성장치에서도 스탯 합산하기