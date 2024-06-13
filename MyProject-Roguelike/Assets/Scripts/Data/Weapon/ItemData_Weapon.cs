using System;
using Unity.VisualScripting;

using UnityEngine;

// 아이템 한 종류의 정보를 저장하는 스크립터블 오브젝트
[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Weapon Data", order = 1)]
public class ItemData_Weapon :  ItemData, IWeapon
{
    public uint maxStackCount = 1;

    [Header("무기 기본 정보")]
    public uint weaponDamage;
    public float attackSpeed;
    public float criticalHit;
    public float attackDuration;
    public float weaponCoolTime;
    public Sprite icon;
    public GameObject modelPrefab;

    public uint AttackPower => weaponDamage;

    public int GetWeaponDamage()
    {
        return (int)weaponDamage;
    }

    public float GetAttackSpeed()
    {
        return (float)attackSpeed;
    }

    public float GetCriticalHit()
    {
        return (float)criticalHit;
    }
}
