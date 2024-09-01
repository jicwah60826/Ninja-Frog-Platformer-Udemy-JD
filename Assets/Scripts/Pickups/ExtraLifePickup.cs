using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifePickup : MonoBehaviour
{
    public int lifeToAdd;
    private bool _isCollected;
    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_isCollected)
            {
                LifeManager.instance.AddLifeCollectible(lifeToAdd);
                _isCollected = true;

                if (pickupEffect != null)
                {
                    Instantiate(pickupEffect, transform.position, transform.rotation);
                }
            }
            Destroy(gameObject);
        }
    }
}
