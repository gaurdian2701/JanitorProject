using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : GenericMonoSingleton<ObjectPoolManager>
{
    [SerializeField] private ProjectilePoolScriptableObject platformBoxProjectilePoolSO;
    [SerializeField] private ProjectilePoolScriptableObject roboGunProjectilePoolSO;

    private ObjectPool platformBoxPool;
    private ObjectPool roboGunProjectilePool;
  
    protected override void Awake()
    {
        base.Awake();

        platformBoxPool = new PlatformBoxPool(platformBoxProjectilePoolSO, this);
        roboGunProjectilePool = new RoboGunProjectilePool(roboGunProjectilePoolSO, this);
    }

    public Tuple<GameObject, int> GetProjectileFromPool(ProjectileType projectile)
    {
        Tuple<GameObject, int> pooledObject; 
        switch (projectile)
        {
            case ProjectileType.PlatformBox:
                pooledObject = platformBoxPool.GetPooledObject();
                break;

            case ProjectileType.RoboGunProjectile:
                pooledObject = roboGunProjectilePool.GetPooledObject();
                break;

            default:
                pooledObject = null;
                break;
        }

        return pooledObject;
    }

    public GameObject InstantiateProjectile(GameObject spawnablePrefab) 
    {
        return Instantiate(spawnablePrefab);
    }
}
