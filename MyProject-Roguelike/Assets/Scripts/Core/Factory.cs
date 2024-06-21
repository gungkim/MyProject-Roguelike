using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    FlyingBatPool flyingBatPool;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        flyingBatPool = GetComponentInChildren<FlyingBatPool>();
        if (flyingBatPool != null) flyingBatPool.Initialize();
    }

    public GameObject CreateWeapon
        (GameObject prefab, Vector3 position)
    {
        Player player = GameManager.Instance.Player;
        GameObject projectile = Instantiate(prefab, player.position, Quaternion.identity);
        
        return projectile;
    }
}
