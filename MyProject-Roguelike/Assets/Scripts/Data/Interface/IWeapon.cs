using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon : IAttack
{
    int GetWeaponDamage();
    float GetAttackSpeed();

    float GetCriticalHit();
}
