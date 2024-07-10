using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_AutoTarget : Weapon_Projectile
{
    private float detectionRadius = 1000.0f;

    protected override void LaunchProjectile()
    {
        Player player = GameManager.Instance.Player;
        if (player == null)
            return;

        Vector3 playerPosition = player.transform.position;

        EnemyBase nearestEnemy = FindingEnemy(playerPosition);
        if (nearestEnemy != null)
        {
            Vector3 enemyDistance = nearestEnemy.transform.position - playerPosition;
            Debug.Log($"{enemyDistance}");
            Vector2 direction = (nearestEnemy.transform.position - playerPosition).normalized;
            float weaponSpeed = itemData_Weapon.attackSpeed + playerStat.AttackSpeed;
            rigidbody2d.velocity = direction * weaponSpeed;
            Debug.Log($"weaponSpeed = {weaponSpeed}");
        }
        else if(nearestEnemy == null)
        {
            Debug.LogWarning("���� ã�� �� ����!");
        }
    }


    /// <summary>
    /// ���� ����� ���� ã�� �Լ�
    /// </summary>
    /// <returns></returns>
    private EnemyBase FindingEnemy(Vector3 playerPosition)
    {
        EnemyBase nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        // �÷��̾� �ֺ��� ���� Ž���ϱ� ���� ����ĳ��Ʈ ���
        RaycastHit2D[] hits = Physics2D.CircleCastAll(playerPosition, detectionRadius, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            EnemyBase enemy = hit.collider.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                float distance = Vector2.Distance(playerPosition, enemy.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }
}
