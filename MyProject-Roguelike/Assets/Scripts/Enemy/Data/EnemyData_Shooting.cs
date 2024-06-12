using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Object/Enemy Shooting Data", order = 1)]
public class EnemyData_Shooting : EnemyData
{
    [Header("�߻�ü ����")]
    public float shootingSpeed;
    public float shootingRange;
    public GameObject bulletPrefab;
}
