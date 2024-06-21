using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_AutoTarget : Weapon_Projectile
{
    private float detectionRadius = 100.0f;

    /// <summary>
    /// 무기의 발사 함수
    /// </summary>
    protected override void LaunchProjectile()
    {
        Player player = GameManager.Instance.Player;
        if (player == null)
            return;

        Vector3 playerPosition = player.transform.position;

        EnemyBase nearestEnemy = FindNearestEnemy(playerPosition);
        if (nearestEnemy != null)
        {
            Vector2 direction = (nearestEnemy.transform.position - playerPosition).normalized;
            GameObject weaponProjectile = Factory.Instance.CreateWeapon(itemData_Weapon.modelPrefab, playerPosition);
            Rigidbody2D projectileRb = weaponProjectile.GetComponent<Rigidbody2D>();

            if (projectileRb != null)
            {
                projectileRb.velocity = direction * (itemData_Weapon.attackSpeed + playerStat.AttackSpeed);
            }
        }
    }


    /// <summary>
    /// 가장 가까운 적을 찾는 함수
    /// </summary>
    /// <returns></returns>
    private EnemyBase FindNearestEnemy(Vector3 playerPosition)
    {
        EnemyBase nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        // 플레이어 주변의 적을 탐지하기 위해 레이캐스트 사용
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
