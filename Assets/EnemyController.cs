using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int damageAmount;

    [SerializeField]
    int damageSFXIndex;

    [SerializeField]
    int deathSFXIndex;
    private Animator anim;

    [HideInInspector]
    public bool isDefeated;

    [SerializeField]
    private float waitToDestroy;

    [SerializeField]
    private int fruitPenalty;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDefeated)
        {
            waitToDestroy -= Time.deltaTime;

            if (waitToDestroy <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isDefeated)
        {
            PlayerHealthController.instance.DamagePlayer(damageAmount);
            AudioManager.instance.PlaySFXPitched(damageSFXIndex); // Player Damage
            if (fruitPenalty > 0)
            {
                // take away fruit per hit if enemy gives fruit penalty
                CollectiblesManager.instance.CollectiblePenalty(fruitPenalty);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy dead!");
            AudioManager.instance.PlaySFX(deathSFXIndex);
            FindFirstObjectByType<PlayerController>().Jump();
            // take awy fruit
            anim.SetTrigger("defeated");
            isDefeated = true;
        }
    }
}
