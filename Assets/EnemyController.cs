using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int damageAmount;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer(damageAmount);
            AudioManager.instance.PlaySFX(19); // Player Damage
        }
    }
}
