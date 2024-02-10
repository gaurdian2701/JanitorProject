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

    //Function to change usability from usable to unusable and vice versa
    protected virtual void ChangeUsability(int i, Usability usability) { usabilityList[i] = usability;} 

    protected void InitializeList()
    {
        objectList = new List<Tuple<GameObject, int>>(listSize); //First list - tuple of <projectile, projectile index>
        usabilityList = new List<Usability>(listSize); //Second list - list of usability state of each projectile, accessed using the projectile index

        for (int i = 0; i < objectList.Capacity; i++)
        {
            GameObject obj = poolManager.InstantiateProjectile(spawnablePrefab); //Asking the PoolManager to instantiate given projectile
            obj.SetActive(false);
            var t = new Tuple<GameObject, int>(obj, i);
            objectList.Add(t);
            usabilityList.Add(Usability.Usable); //Adding the object to both lists and keeping it usable by default.
        }
    }

    //ONLY WHEN THE OBJECT IS USABLE, WILL IT GET RETURNED, IF IT IS NOT, FOR E.G. IF IT IS SUCKED IN BY THE PLAYER, THEN IT WONT BE RETURNED.
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
