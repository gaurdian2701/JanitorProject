using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : GenericMonoSingleton<ObjectPool>
{
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private int listSize;
    public List<Tuple<GameObject, int>> objectList;
    public List<Usability> usabilityList;

    private void Start()
    {
        InitializeList();
        SuckableBase.RenderUsability += ChangeUsability;
    }

    private void OnDestroy()
    {
        SuckableBase.RenderUsability -= ChangeUsability;
    }
    private void ChangeUsability(int i, Usability usability)
    {
        usabilityList[i] = usability;
    }

    private void InitializeList()
    {
        objectList = new List<Tuple<GameObject, int>>(listSize);

        for (int i = 0; i < objectList.Capacity; i++)
        {
            GameObject obj = Instantiate(boxPrefab);
            obj.SetActive(false);
            var t = new Tuple<GameObject, int>(obj, i);
            objectList.Add(t);
            usabilityList.Add(Usability.Usable);
        }
    }

    public Tuple<GameObject, int> GetPooledObject()
    {
        for(int i=0; i< objectList.Capacity; i++)
        {
            if (!objectList[i].Item1.activeInHierarchy && usabilityList[i] == Usability.Usable)
                return objectList[i];
        }
        return null;
    }
}
