using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Object/Enemy Data", order = 0)]
public class EnemyData : ScriptableObject
{
    [Header("적 기본 정보")]
    public EnemyCode code;
    public EnemyType type;
    public string enemyName = "적 이름";
    public string enemyDescription = "적 설명";
    public Sprite sprite;

    [Header("적 스탯")]
    public float chaseSpeed;
    public float enemyDamage;
    public float enemyMaxHP;
    public float enemyDefense;
}
