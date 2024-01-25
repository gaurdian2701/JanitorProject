using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboGunProjectilePool : ObjectPool
{
    [SerializeField] private ProjectilePoolScriptableObject roboGunProjectilePoolSO;
    public RoboGunProjectilePool(ProjectilePoolScriptableObject roboGunProjectilePoolSO, ObjectPoolManager poolManager) : base(poolManager)
    {
        spawnablePrefab = roboGunProjectilePoolSO.spawnablePrefab;
        listSize = roboGunProjectilePoolSO.listSize;
        base.InitializeList();
        SuckableBase.RenderRoboGunProjectileUsability += ChangeUsability;
    }

    private void OnDisable()
    {
        SuckableBase.RenderRoboGunProjectileUsability -= ChangeUsability;
    }
    protected override void ChangeUsability(int i, Usability usability)
    {
        base.usabilityList[i] = usability;
    }
    public override Tuple<GameObject, int> GetPooledObject()
    {
        return base.GetPooledObject();  
    }
}
