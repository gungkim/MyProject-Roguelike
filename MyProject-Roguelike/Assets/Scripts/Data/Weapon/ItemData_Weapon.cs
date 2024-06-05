using System;
using Unity.VisualScripting;

using UnityEngine;

// 아이템 한 종류의 정보를 저장하는 스크립터블 오브젝트
[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Weapon Data", order = 1)]
public class ItemData_Weapon :  IWeapon
{
    public uint maxStackCount = 1;

    [Header("무기 기본 정보")]
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
