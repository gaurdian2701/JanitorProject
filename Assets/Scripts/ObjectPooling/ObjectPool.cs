using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    protected GameObject spawnablePrefab;
    protected int listSize;
    public List<Tuple<GameObject, int>> objectList;
    public List<Usability> usabilityList;

    protected virtual void Awake()
    {
        InitializeList();
    }

    protected virtual void ChangeUsability(int i, Usability usability) { }

    private void InitializeList()
    {
        objectList = new List<Tuple<GameObject, int>>(listSize);

        for (int i = 0; i < objectList.Capacity; i++)
        {
            GameObject obj = Instantiate(spawnablePrefab);
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
