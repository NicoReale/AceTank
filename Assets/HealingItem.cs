using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerBehaviour>();
        if (player != null)
        {
            player.Heal(20);
            Destroy(gameObject);
        }
    }
}
