using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBoxPool : ObjectPool
{
    public PlatformBoxPool(ProjectilePoolScriptableObject platformBoxPoolSO, ObjectPoolManager poolManager) : base(poolManager)
    {
        spawnablePrefab = platformBoxPoolSO.spawnablePrefab;
        listSize = platformBoxPoolSO.listSize;
        InitializeList();
        SuckableBase.RenderPlatformBoxUsability += ChangeUsability;
    }

    private void OnDisable()
    {
        SuckableBase.RenderPlatformBoxUsability -= ChangeUsability;
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
