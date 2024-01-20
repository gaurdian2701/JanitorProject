using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboGunProjectilePool : ObjectPool
{
    [SerializeField] private ProjectilePoolScriptableObject roboGunProjectilePoolSO;
    protected override void Awake()
    {
        spawnablePrefab = roboGunProjectilePoolSO.spawnablePrefab;
        listSize = roboGunProjectilePoolSO.listSize;
        base.Awake();
        SuckableBase.RenderRoboGunProjectileUsability += ChangeUsability;
    }

    private void OnDestroy()
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
