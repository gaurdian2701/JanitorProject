using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : GenericMonoSingleton<ObjectPoolManager>
{
    [SerializeField] private ObjectPool platformBoxPool;
    protected override void Awake()
    {
        base.Awake();
    }

    public Tuple<GameObject, int> GetProjectileFromPool(ProjectileType projectile)
    {
        Tuple<GameObject, int> pooledObject; 
        switch (projectile)
        {
            case ProjectileType.PlatformBox:
                pooledObject = platformBoxPool.GetPooledObject();
                break;
            default:
                pooledObject = null;
                break;
        }

        return pooledObject;
    }
}
