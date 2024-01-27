using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShooter : MonoBehaviour
{
    [SerializeField] private float shootFrequency;
    [SerializeField] private ProjectileType projectileRequired;
    [SerializeField] private Transform shootPos;

    private void OnEnable()
    {
        InvokeRepeating(nameof(ShootBox), 0f, shootFrequency);
    }

    private void ShootBox()
    {
        var tuple = ObjectPoolManager.Instance.GetProjectileFromPool(projectileRequired);
        Debug.Log(tuple.ToString());
        if (tuple == null)
            return;

        GameObject box = tuple.Item1;
        if (box && box.TryGetComponent(out SuckableObjectStateManager boxState))
        {
            box.SetActive(true);
            boxState.SetLauncher(this.gameObject);
            boxState.SetUsabilityIndex(tuple.Item2);    
            boxState.SwitchToShoot(shootPos);
        }
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(ShootBox)); 
    }
}
