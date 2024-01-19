using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletShooter : MonoBehaviour
{
    [SerializeField] private float shootFrequency;

    private void Awake()
    {
        InvokeRepeating(nameof(ShootBox), 0f, shootFrequency);
    }

    private void ShootBox()
    {
        var tuple = ObjectPool.Instance.GetPooledObject();
        if (tuple == null)
            return;

        GameObject box = tuple.Item1;
        if (box && box.TryGetComponent(out SuckableObjectStateManager boxState))
        {
            box.SetActive(true);
            boxState.SetLauncher(this.gameObject);
            boxState.SetUsabilityIndex(tuple.Item2);    
            boxState.SwitchToShoot();
        }
    }
}
