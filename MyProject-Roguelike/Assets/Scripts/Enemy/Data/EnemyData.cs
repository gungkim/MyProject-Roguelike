using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Object/Enemy Data", order = 0)]
public class EnemyData : ScriptableObject
{
    [Header("�� �⺻ ����")]
    public EnemyCode code;
    public EnemyType type;
    public string enemyName = "�� �̸�";
    public string enemyDescription = "�� ����";
    public Sprite sprite;

    [Header("�� ����")]
    public float chaseSpeed;
    public float enemyDamage;
    public float enemyMaxHP;
    public float enemyDefense;
}
