using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderManager : MonoBehaviour
{
    public bool ignore;

    [SerializeField] private CircleCollider2D plungerSuckCollider;

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
            obj.sucker = controller;
            obj.SwitchToSuck();
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
