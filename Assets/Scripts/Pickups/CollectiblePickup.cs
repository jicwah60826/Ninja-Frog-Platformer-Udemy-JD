using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickup : MonoBehaviour
{
    [SerializeField]
    private int collectibleAmount;

    [SerializeField]
    private GameObject pickupEffect;

    private bool _isCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_isCollected)
            {

                CollectiblesManager.instance.GetCollectible(collectibleAmount);
                Debug.Log(collectibleAmount);
                _isCollected = true;
                AudioManager.instance.PlaySFXPitched(3); // Pickup sound
                Instantiate(pickupEffect, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
