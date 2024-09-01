using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHeartPickup : MonoBehaviour
{
    private bool _isCollected;
    public GameObject pickupEffect;

    [SerializeField]
    private int heartsToAdd;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (
            !_isCollected && PlayerHealthController.instance.maxHealth < 20)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                {
                    Debug.Log("hearts added: " + heartsToAdd);
                    PlayerHealthController.instance.AddHearts(heartsToAdd);
                    Instantiate(pickupEffect, transform.position, transform.rotation);
                    AudioManager.instance.PlaySFX(4); // Heal player
                }
            }
            _isCollected = true;
            Destroy(gameObject);
        }
    }
}
