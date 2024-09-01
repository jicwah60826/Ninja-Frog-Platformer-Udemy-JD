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

    [SerializeField]
    private bool assignRandomValue;

    [SerializeField]
    private int lowRange, highRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_isCollected)
            {
                if (assignRandomValue)
                {
                    collectibleAmount = Random.Range(lowRange, highRange); // we will get results from 1.0 to 3.0
                }

                CollectiblesManager.instance.GetCollectible(collectibleAmount);
                Debug.Log(collectibleAmount);
                _isCollected = true;
                AudioManager.instance.PlaySFX(3); // Pickup sound
                Instantiate(pickupEffect, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
