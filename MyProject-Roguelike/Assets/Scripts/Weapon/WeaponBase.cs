using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �߰�����
/// </summary>
[System.Serializable]
public struct WeaponInfo
{
    public uint weaponDamage;
    public float attackSpeed;
    public float criticalHit;
    public GameObject modelPrefab;
}
public class WeaponBase : MonoBehaviour
{
    [SerializeField]
    private ItemData_Weapon weaponData;

    PlayerStat playerStat;

    public float totalDamage => weaponData.Weaponinfo.weaponDamage + playerStat.AttackPower;


}
