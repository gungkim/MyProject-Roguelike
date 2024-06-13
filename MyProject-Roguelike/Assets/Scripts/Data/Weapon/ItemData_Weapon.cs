using System;
using Unity.VisualScripting;

using UnityEngine;

// ������ �� ������ ������ �����ϴ� ��ũ���ͺ� ������Ʈ
[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Weapon Data", order = 1)]
public class ItemData_Weapon :  ItemData, IWeapon
{
    public uint maxStackCount = 1;

    [Header("���� �⺻ ����")]
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
