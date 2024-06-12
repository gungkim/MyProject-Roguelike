using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Data", order = 5)]
public class ItemData_Accessory : ItemData
{
    [Header("장신구 정보")]
    public float moveSpeed;
    public float damage;
    public float coolDown;
    public float attackSpeed;
    public float maxHP;
    public int defense;
    public float attackRange;
    public float criticalChance;
    public float expGain;
}
