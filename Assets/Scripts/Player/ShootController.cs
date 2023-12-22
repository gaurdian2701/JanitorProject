using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private int objectNumber;

    private PlayerColliderManager playerColliderManager;
    private List<GameObject> suckedObjects;
    private bool canShoot;

    private void Awake()
    {
        SuckableBase.objectSucked += HandleSuckedObjects;
        suckedObjects = new List<GameObject>();
        playerColliderManager = GetComponent<PlayerColliderManager>();

        canShoot = true;
    }

    private void Start()
    {
        playerColliderManager.SetObjectLimit(objectNumber);
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

        playerColliderManager.SetObjectCount(suckedObjects.Count);
    }

    private void EnableComponents(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().enabled = true;
        obj.GetComponent<BoxCollider2D>().enabled = true;
        obj.GetComponent<SuckableObjectStateManager>().enabled = true;
    }

    private void DisableComponents(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().enabled = false;
        obj.GetComponent<BoxCollider2D>().enabled = false;
        obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        obj.GetComponent<SuckableObjectStateManager>().enabled = false;
    }


    public void ReleaseSuckedObjects()
    {
        if (SuckedObjectsListEmpty() || !canShoot)
            return;

        GameObject obj = suckedObjects[suckedObjects.Count - 1];
        EnableComponents(obj);
        obj.GetComponent<SuckableObjectStateManager>().SwitchToShoot();
        suckedObjects.Remove(obj);

        playerColliderManager.SetObjectCount(suckedObjects.Count);
    }

    public bool SuckedObjectsListEmpty()
    {
        if (suckedObjects.Count == 0)
            return true;

        return false;
    }

    public void SetShootCondition(bool condition)
    {
        canShoot = condition;
    }

    public bool CanShoot()
    {
        return canShoot;
    }
}
