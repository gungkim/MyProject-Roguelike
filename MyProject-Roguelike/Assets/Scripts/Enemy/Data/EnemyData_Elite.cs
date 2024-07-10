using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Object/Enemy Elite Data", order = 2)]
public class EnemyData_Elite : EnemyData
{
    [Header("엘리트 몹 설정")]
    public bool isElite;
    public int extraExp; // 추가 경험치
    public float dropChance; // 아이템 드롭 확률
}
