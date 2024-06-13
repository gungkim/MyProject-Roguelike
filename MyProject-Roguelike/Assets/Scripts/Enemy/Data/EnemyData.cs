using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Object/Enemy Data", order = 0)]
public class EnemyData : ScriptableObject, IAttack
{
    [Header("적 기본 정보")]
    public EnemyCode code;
    public EnemyType type;
    public string enemyName = "적 이름";
    public string enemyDescription = "적 설명";
    public Sprite sprite;

    [Header("적 스탯")]
    public float chaseSpeed;
    public int enemyDamage;
    public int enemyMaxHP;
    public int enemyDefense;

    public uint AttackPower => (uint)enemyDamage;
}
