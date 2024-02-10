using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShooter : MonoBehaviour
{
    [SerializeField] private float shootFrequency;
    [SerializeField] private ProjectileType projectileRequired;
    [SerializeField] private Transform shootPos;
    [SerializeField] private ObjectPoolManager objectPoolManager;

    private void OnEnable()
    {
        InvokeRepeating(nameof(ShootProjectile), 0f, shootFrequency);
    }

    private void ShootProjectile()
    {
        var tuple = objectPoolManager.GetProjectileFromPool(projectileRequired); //Get projectile from pool
        if (tuple == null)
            return;

        GameObject box = tuple.Item1;
        if (box && box.TryGetComponent(out SuckableObjectStateManager boxState)) //Enable projectile scripts and set launching location
        {
            box.SetActive(true);
            boxState.SetLauncher(this.gameObject);
            boxState.SetUsabilityIndex(tuple.Item2);    
            boxState.SwitchToShoot(shootPos);
        }
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(ShootProjectile)); 
    }
}
