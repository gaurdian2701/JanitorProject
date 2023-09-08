using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderManager : MonoBehaviour
{
    [SerializeField] private CircleCollider2D plungerSuckCollider;
    private void Start()
    {
        plungerSuckCollider.enabled = false;
    }
    public void EnablePlungerSuckCollider()
    {
        plungerSuckCollider.enabled = true;
    }

    public void DisablePlungerSuckCollider()
    {
        plungerSuckCollider.enabled = false;
    }
}
