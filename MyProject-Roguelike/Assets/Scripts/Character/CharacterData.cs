using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "Scriptable Object/Character Data", order = 0)]
public class CharacterData : ScriptableObject, IAttack
{
    [Header("ĳ���� ����")]
    public string characterName;
    public Sprite characterImage;
    public GameObject character;

    [Header("ĳ���� ����")]
    public int playerAttackPower;
    public float moveSpeed;
    public float criticalChance;
    public float attackRange;
    public float coolDown;
    public float maxHP;
    public int defense;
    public float attackDuration;
    public float attackSpeed;
    public int expGain;

    public uint AttackPower => (uint)playerAttackPower;

    public float CriticalChance => criticalChance;
}
