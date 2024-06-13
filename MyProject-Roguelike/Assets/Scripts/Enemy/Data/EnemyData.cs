using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Object/Enemy Data", order = 0)]
public class EnemyData : ScriptableObject, IAttack
{
    [Header("�� �⺻ ����")]
    public EnemyCode code;
    public EnemyType type;
    public string enemyName = "�� �̸�";
    public string enemyDescription = "�� ����";
    public Sprite sprite;

    [Header("�� ����")]
    public float chaseSpeed;
    public int enemyDamage;
    public int enemyMaxHP;
    public int enemyDefense;

    public uint AttackPower => (uint)enemyDamage;
}
