using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuckedObjectsController : MonoBehaviour
{
    private List<GameObject> suckedObjects;
    private void Awake()
    {
        SuckableBase.objectSucked += HandleSuckedObjects;
        suckedObjects = new List<GameObject>();
    }

    private void OnDestroy()
    {
        SuckableBase.objectSucked -= HandleSuckedObjects;
        suckedObjects.Clear();
    }

    private void HandleSuckedObjects(GameObject obj)
    {
        DisableComponents(obj);
        obj.transform.parent = transform;
        suckedObjects.Add(obj);
    }

    private void EnableComponents(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().enabled = true;
        obj.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void DisableComponents(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().enabled = false;
        obj.GetComponent<BoxCollider2D>().enabled = false;
        obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }


    public void ReleaseSuckedObjects()
    {
        if (SuckedObjectsListEmpty())
            return;

        GameObject obj = suckedObjects[suckedObjects.Count - 1];
        EnableComponents(obj);
        obj.GetComponent<SuckableObjectStateManager>().SwitchToShoot();
        suckedObjects.Remove(obj);
    }

    public bool SuckedObjectsListEmpty()
    {
        if (suckedObjects.Count == 0)
            return true;

        return false;
    }
}
