using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon_Projectile_LookAt : WeaponBase
{
    new GameObject gameObject;

    /// <summary>
    /// 플레이어의 인풋 방향
    /// </summary>
    private Vector2 lastInputDirection;

    private bool canFire = true;

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 무기의 발사 함수
    /// </summary>
    protected override void Fire()
    {
        if (!canFire)
            return;

        Player player = GameManager.Instance.Player;

        Vector2 direction = player.InputDirection != Vector2.zero ? player.InputDirection : lastInputDirection;

        if (direction != Vector2.zero)
        {
            lastInputDirection = direction; // 마지막 입력 방향 업데이트

            GameObject weaponProjectile = Factory.Instance.CreateWeapon(itemData_Weapon.modelPrefab, transform.position);
            Rigidbody2D projectileRb = weaponProjectile.GetComponent<Rigidbody2D>();

            if (projectileRb != null)
            {
                projectileRb.velocity = direction.normalized * (itemData_Weapon.attackSpeed + playerStat.AttackSpeed);
            }
        }

        StartCoroutine(CooldownRoutine());
    }

    /// <summary>
    /// 쿨타임동안 발사되지 않게끔 하는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator CooldownRoutine()
    {
        canFire = false;
        CalculateCoolTime();
        
        yield return new WaitForSeconds(coolTime);
        canFire = true;
    }
}
