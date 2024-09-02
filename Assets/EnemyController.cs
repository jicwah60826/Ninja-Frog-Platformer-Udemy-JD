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
            AudioManager.instance.PlaySFX(damageSFXIndex); // Player Damage
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy dead!");
            AudioManager.instance.PlaySFX(deathSFXIndex);
            FindFirstObjectByType<PlayerController>().Jump();
            anim.SetTrigger("defeated");
            isDefeated = true;
        }
    }
}
