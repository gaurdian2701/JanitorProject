using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuckController : MonoBehaviour
{
    public bool ignore;

    [SerializeField] private CircleCollider2D plungerSuckCollider;
    [SerializeField] private Transform suckPosition;

    private float objectCount;
    private bool colliderLocked;
    private float objectLimit;

    private PlayerController controller;
    private SuckableObjectStateManager obj;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        colliderLocked = false;
        objectCount = 0;
        ignore = false;
    }
    private void Start()
    {
        plungerSuckCollider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        obj = collision.gameObject.GetComponent<SuckableObjectStateManager>();


        if (obj && !obj.isSucked && !ignore)
        {
            obj.SetLauncher(controller.gameObject);
            obj.SwitchToSuck(suckPosition);
            ignore = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suckable"))
        {
            ignore = false;
        }
    }

    public void EnablePlungerSuckCollider()
    {
        if (colliderLocked)
            return;

        plungerSuckCollider.enabled = true;
    }

    public void DisablePlungerSuckCollider()
    {
        plungerSuckCollider.enabled = false;
    }

    //The following functions are so that this script can know the number of objects and then block the sucking collider from taking any more objects
    //once the limit has been reached.
    //TODO: why not just handle this using a single event listener instead of having to call this every godddamn time an object gets sucked ffs.
    public void SetObjectLimit(int amount)
    {
        objectLimit = amount;
    }

    public void SetObjectCount(int amount) 
    {
        objectCount = amount;

        if (objectCount >= objectLimit)
            colliderLocked = true;
        else
            colliderLocked = false;
    }
}
