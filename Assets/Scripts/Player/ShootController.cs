using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private int objectNumber;

    private PlayerColliderManager playerColliderManager;
    [SerializeField] private List<GameObject> suckedObjects;
    [SerializeField] private bool canShoot;

    private void Awake()
    {
        SuckableBase.ObjectSucked += HandleSuckedObjects;
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
        SuckableBase.ObjectSucked -= HandleSuckedObjects;
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
        obj.SetActive(true);
    }

    private void DisableComponents(GameObject obj)
    {
        obj.SetActive(false);
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
        Debug.Log("shoot set to" + condition);
        canShoot = condition;
    }

    public bool CanShoot()
    {
        return canShoot;
    }
}
