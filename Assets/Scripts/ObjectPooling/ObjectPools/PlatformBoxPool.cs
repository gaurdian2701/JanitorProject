using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBoxPool : ObjectPool
{
    [SerializeField] private ProjectilePoolScriptableObject platformBoxPoolSO;
    protected override void Awake()
    {
        spawnablePrefab = platformBoxPoolSO.spawnablePrefab;
        listSize = platformBoxPoolSO.listSize;
        base.Awake();
        SuckableBase.RenderPlatformBoxUsability += ChangeUsability;
    }

    private void OnDestroy()
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
