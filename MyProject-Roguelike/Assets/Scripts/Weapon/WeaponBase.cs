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

    protected virtual void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        playerStat = FindObjectOfType<PlayerStat>();

        if(playerStat != null)
        {
            playerStat.OnStatsChanged += CalculateCoolTime;
        }

        CalculateCoolTime();

        StartCoroutine(WeaponActivate());
    }

    protected IEnumerator WeaponActivate()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(coolTime);
        }
    }
    
    protected virtual void Fire()
    {

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Attack(collision);
        }
    }


    protected virtual void Attack(Collider2D collision)
    {
        int weaponDamage = GetWeaponDamage();
        int playerAttackPower = (int)playerStat.PlayerAttackPower;

        totalDamage = weaponDamage + playerAttackPower;

        // ũ��Ƽ�� ��Ʈ üũ
        if (CriticalHit())
        {
            totalDamage = (int)(totalDamage * 2); // ũ��Ƽ�� ������ 2��
            Debug.Log("Critical Hit!");
        }
    }

    protected virtual bool CriticalHit()
    {
        float weaponCritical = GetCriticalHit();
        float playerCritical = playerStat.CriticalChance;
        float totalCritical = weaponCritical + playerCritical;
        float randomValue = UnityEngine.Random.value * 100f;
        return randomValue <= totalCritical;
    }

    private void CalculateCoolTime()
    {
        if (playerStat != null && itemData_Weapon != null)
        {
            coolTime = itemData_Weapon.weaponCoolTime * (1 - (playerStat.coolTime / 100f));
        }
        else if (itemData_Weapon != null)
        {
            coolTime = itemData_Weapon.weaponCoolTime;
        }
        else
        {
            coolTime = 0; // ���� ó��: ���� �����Ͱ� ���� ���
        }
    }

    public uint AttackPower
    {
        get { return (uint)GetWeaponDamage(); }
    }

    public int GetWeaponDamage()
    {
        return (int)itemData_Weapon.weaponDamage;
    }

    public float GetAttackSpeed()
    {
        return itemData_Weapon.attackSpeed;
    }

    public float GetCriticalHit()
    {
        return itemData_Weapon.criticalHit;
    }
}
