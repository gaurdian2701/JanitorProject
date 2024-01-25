using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private ObjectPoolManager poolManager;

    protected GameObject spawnablePrefab;
    protected int listSize;

    public List<Tuple<GameObject, int>> objectList;
    public List<Usability> usabilityList;

    public ObjectPool(ObjectPoolManager _poolManager)
    {
        poolManager = _poolManager;
    }
    protected virtual void ChangeUsability(int i, Usability usability) { }

    protected void InitializeList()
    {
        objectList = new List<Tuple<GameObject, int>>(listSize);
        usabilityList = new List<Usability>(listSize);

        for (int i = 0; i < objectList.Capacity; i++)
        {
            GameObject obj = poolManager.InstantiateProjectile(spawnablePrefab);
            obj.SetActive(false);
            var t = new Tuple<GameObject, int>(obj, i);
            objectList.Add(t);
            usabilityList.Add(Usability.Usable);
        }
    }
    public virtual Tuple<GameObject, int> GetPooledObject()
    {
        for (int i = 0; i < objectList.Capacity; i++)
        {
            if (!objectList[i].Item1.activeInHierarchy && usabilityList[i] == Usability.Usable)
                return objectList[i];
        }
        return null;
    }
}
