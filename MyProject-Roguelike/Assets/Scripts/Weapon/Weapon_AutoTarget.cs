using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_AutoTarget : WeaponBase
{
    private bool canFire = true;
    private float detectionRadius = 10f;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// ������ �߻� �Լ�
    /// </summary>
    protected override void Fire()
    {
        if (!canFire)
            return;

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

        StartCoroutine(CooldownRoutine());
    }

    /// <summary>
    /// ��Ÿ�ӵ��� �߻���� �ʰԲ� �ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator CooldownRoutine()
    {
        canFire = false;
        Debug.Log($"Cooldown started. CoolTime: {coolTime}");

        yield return new WaitForSeconds(coolTime);
        canFire = true;
        Debug.Log("Cooldown ended");
    }

    /// <summary>
    /// ���� ����� ���� ã�� �Լ�
    /// </summary>
    /// <returns></returns>
    private EnemyBase FindNearestEnemy(Vector3 playerPosition)
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
