using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHealthPickup : MonoBehaviour
{
    private bool _isCollected;
    public GameObject pickupEffect;

    [SerializeField]
    private int healAmount;

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
                {
                    Debug.Log("Player received " + healAmount + " Health");
                    PlayerHealthController.instance.HealPlayer(healAmount);
                    Instantiate(pickupEffect, transform.position, transform.rotation);
                    AudioManager.instance.PlaySFX(4); // Heal player
                }
            }
            _isCollected = true;
            Destroy(gameObject);
        }
    }
}
