using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    FlyingBatPool flyingBatPool;
    FireBallPool fireBallPool;
    IceShotPool iceShotPool;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        flyingBatPool = GetComponentInChildren<FlyingBatPool>();
        if (flyingBatPool != null) flyingBatPool.Initialize();

        fireBallPool = GetComponentInChildren<FireBallPool>();
        if (fireBallPool != null) fireBallPool.Initialize();

        iceShotPool = GetComponentInChildren<IceShotPool>();
        if (iceShotPool != null) iceShotPool.Initialize();
    }

    public GameObject CreateWeapon
        (GameObject prefab, Vector3 position)
    {
        Player player = GameManager.Instance.Player;
        GameObject projectile = Instantiate(prefab, player.position, Quaternion.identity);
        
        return projectile;
    }
}
