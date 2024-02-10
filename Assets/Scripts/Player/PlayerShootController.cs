using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    [SerializeField] private int objectNumber;

    private PlayerSuckController playerColliderManager;
    [SerializeField] private List<SuckableObjectStateManager> suckedObjects;
    [SerializeField] private bool canShoot;
    [SerializeField] private Transform shootPos;

    private void Awake()
    {
        EventService.Instance.OnObjectSucked.AddEventListener(HandleSuckedObjects);
        suckedObjects = new List<SuckableObjectStateManager>();
        playerColliderManager = GetComponent<PlayerSuckController>();

        canShoot = true;
    }

    private void Start()
    {
        playerColliderManager.SetObjectLimit(objectNumber);
    }

    private void OnDestroy()
    {
        EventService.Instance.OnObjectSucked.RemoveEventListener(HandleSuckedObjects);
        suckedObjects.Clear();
    }

    private void HandleSuckedObjects(SuckableObjectStateManager obj)
    {
        DisableComponents(obj.gameObject);
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

        SuckableObjectStateManager obj = suckedObjects[suckedObjects.Count - 1];

        EnableComponents(obj.gameObject);
        obj.SwitchToShoot(shootPos);
        suckedObjects.Remove(obj);
        EventService.Instance.OnObjectShot.InvokeEvent();

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
