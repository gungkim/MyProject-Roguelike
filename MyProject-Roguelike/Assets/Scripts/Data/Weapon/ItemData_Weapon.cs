using System;
using Unity.VisualScripting;

using UnityEngine;

// ������ �� ������ ������ �����ϴ� ��ũ���ͺ� ������Ʈ
[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Weapon Data", order = 1)]
public class ItemData_Weapon :  IWeapon
{
    public uint maxStackCount = 1;

    [Header("���� �⺻ ����")]
    public WeaponInfo Weaponinfo;
    
    public int GetWeaponDamage()
    {
        return (int)Weaponinfo.weaponDamage;
    }

    public float GetAttackSpeed()
    {
        return (float)Weaponinfo.attackSpeed;
    }

    public WeaponInfo GetWeaponInfo()
    {
        return Weaponinfo;
    }
}
