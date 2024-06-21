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
    /// ����ü ������ �߻� �Լ�
    /// </summary>
    protected abstract void LaunchProjectile();
}
