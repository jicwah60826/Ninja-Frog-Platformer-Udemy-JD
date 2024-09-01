using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHealthRestore : MonoBehaviour
{
    private bool _isCollected;
    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (
            !_isCollected
            && PlayerHealthController.instance.currentHealth
                < PlayerHealthController.instance.maxHealth
        )
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player health fully restored");
                PlayerHealthController.instance.FullHealthRestore();
                Instantiate(pickupEffect, transform.position, transform.rotation);
                AudioManager.instance.PlaySFX(4); // Heal player
            }
            _isCollected = true;
            Destroy(gameObject);
        }
    }
}
