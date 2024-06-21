using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon_Projectile : WeaponBase
{
    protected override void Fire()
    {
        LaunchProjectile();
    }

    /// <summary>
    /// 투사체 무기의 발사 함수
    /// </summary>
    protected abstract void LaunchProjectile();
}
