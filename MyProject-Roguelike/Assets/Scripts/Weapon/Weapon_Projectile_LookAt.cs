using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon_Projectile_LookAt : WeaponBase
{
    new GameObject gameObject;

    private Vector2 lastInputDirection;

        private bool canFire = true;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Fire()
    {
        if (!canFire)
            return;

        Player player = GameManager.Instance.Player;

        Vector2 direction = player.InputDirection != Vector2.zero ? player.InputDirection : lastInputDirection;

        if (direction != Vector2.zero)
        {
            lastInputDirection = direction; // 마지막 입력 방향 업데이트

            GameObject weaponProjectile = Factory.Instance.CreateWeaponProjectile(itemData_Weapon.modelPrefab, transform.position);
            Rigidbody2D projectileRb = weaponProjectile.GetComponent<Rigidbody2D>();

            if (projectileRb != null)
            {
                projectileRb.velocity = direction.normalized * (itemData_Weapon.attackSpeed + playerStat.AttackSpeed);
            }
        }

        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        canFire = false;
        CalculateCoolTime();
        
        yield return new WaitForSeconds(coolTime);
        canFire = true;
    }
}
