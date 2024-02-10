using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
            EventService.Instance.OnLevelComplete.InvokeEvent();
    }
}
