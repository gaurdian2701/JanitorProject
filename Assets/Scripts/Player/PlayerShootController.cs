using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    [SerializeField] private int objectNumber;

    private PlayerSuckController playerColliderManager;
    [SerializeField] private List<GameObject> suckedObjects;
    [SerializeField] private bool canShoot;
    [SerializeField] private Transform shootPos;

    private void Awake()
    {
        SuckableBase.ObjectSucked += HandleSuckedObjects;
        suckedObjects = new List<GameObject>();
        playerColliderManager = GetComponent<PlayerSuckController>();

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
        obj.GetComponent<SuckableObjectStateManager>().SwitchToShoot(shootPos);
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
