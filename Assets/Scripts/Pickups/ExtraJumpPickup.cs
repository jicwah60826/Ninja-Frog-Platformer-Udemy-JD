using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraJumpPickup : MonoBehaviour
{
    public GameObject pickupEffect;

    public PlayerStats stats;
    private bool _isCollected;

    [SerializeField]
    private int jumpsToAdd;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isCollected)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                {
                    Debug.Log("jumps added: " + jumpsToAdd);
                    stats.additionalJumps += jumpsToAdd;
                    Instantiate(pickupEffect, transform.position, transform.rotation);
                }
            }
            _isCollected = true;
            Destroy(gameObject);
        }
    }
}
