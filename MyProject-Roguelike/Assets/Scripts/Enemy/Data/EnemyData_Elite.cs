using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Object/Enemy Elite Data", order = 2)]
public class EnemyData_Elite : EnemyData
{
    [Header("����Ʈ �� ����")]
    public bool isElite;
    public int extraExp; // �߰� ����ġ
    public float dropChance; // ������ ��� Ȯ��
}
