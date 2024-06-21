using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponBase : RecycleObject, IWeapon
{
    protected Rigidbody2D rigidbody2d;

    public ItemData_Weapon itemData_Weapon;
    protected PlayerStat playerStat;

    protected int totalDamage;
    protected float coolTime;

    protected bool isCriticalActive = false;

    protected bool isAttack = true;

    protected virtual void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        playerStat = FindObjectOfType<PlayerStat>();

        if (playerStat != null)
        {
            playerStat.OnStatsChanged += CalculateCoolTime;
        }

        CalculateCoolTime();
    }

    /// <summary>
    /// 무기가 작동하는 함수(쿨타임엔 작동안하게끔)
    /// </summary>
    protected virtual void Fire()
    {
        if (!isAttack)
            return;
        StartCoroutine(CooldownRoutine());
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Attack(collision);
            Destroy(gameObject);
        }
    }

    protected virtual void Attack(Collider2D collision)
    {
        int weaponDamage = GetWeaponDamage();
        int playerAttackPower = (int)playerStat.PlayerAttackPower;

        totalDamage = weaponDamage + playerAttackPower;

        // 크리티컬 히트 체크
        if (CriticalHit())
        {
            totalDamage = (int)(totalDamage * 2); // 크리티컬 데미지 2배
            Debug.Log("Critical Hit!");
        }
    }

    /// <summary>
    /// 아이템 데이터와 플레이어 스탯에 따라 크리티컬 확률을 합산하고 적용하는 함수
    /// </summary>
    /// <returns></returns>
    protected virtual bool CriticalHit()
    {
        float weaponCritical = GetCriticalHit();
        float playerCritical = playerStat.CriticalChance;
        float totalCritical = weaponCritical + playerCritical;
        float randomValue = UnityEngine.Random.value * 100f;
        return randomValue <= totalCritical;
    }

    /// <summary>
    /// 아이템 데이터와 플레이어 스탯에 따라 쿨타임을 계산하는 함수
    /// </summary>
    protected virtual void CalculateCoolTime()
    {
        if (playerStat != null && itemData_Weapon != null)
        {
            // coolTime을 퍼센트 단위로 계산
            float coolTimeReduction = playerStat.CoolTime / 100f;
            coolTime = itemData_Weapon.weaponCoolTime * (1 - coolTimeReduction);
        }
        else if (itemData_Weapon != null)
        {
            coolTime = itemData_Weapon.weaponCoolTime;
        }
        else
        {
            coolTime = 0; // 예외 처리: 무기 데이터가 없을 경우
        }
        Debug.Log($"Calculated coolTime: {coolTime}");
    }

    protected IEnumerator CooldownRoutine()
    {
        isAttack = false;
        CalculateCoolTime();
        yield return new WaitForSeconds(coolTime);
        isAttack = true;
    }

    public uint AttackPower
    {
        get { return (uint)GetWeaponDamage(); }
    }

    public int GetWeaponDamage()
    {
        return (int)itemData_Weapon.weaponDamage;
    }

    public float GetCriticalHit()
    {
        return itemData_Weapon.criticalHit;
    }
}
