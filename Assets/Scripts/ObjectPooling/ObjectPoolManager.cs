using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : GenericMonoSingleton<ObjectPoolManager>  //GenericMonoSingleton base class for all Singletons in the game
{
    [SerializeField] private ProjectilePoolScriptableObject platformBoxProjectilePoolSO;
    [SerializeField] private ProjectilePoolScriptableObject roboGunProjectilePoolSO;

    private ObjectPool platformBoxPool;
    private ObjectPool roboGunProjectilePool;
  
    protected override void Awake()
    {
        base.Awake();

        platformBoxPool = new PlatformBoxPool(platformBoxProjectilePoolSO, this);
        roboGunProjectilePool = new RoboGunProjectilePool(roboGunProjectilePoolSO, this); //Initialization of different projectile pools
    }

    public Tuple<GameObject, int> GetProjectileFromPool(ProjectileType projectile) //Returns a projectile from respective pool depending on projectileType
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
