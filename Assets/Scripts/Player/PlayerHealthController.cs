using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;
    public GameObject deathEffect;
    public GameObject respawnEffect;

    // Invicibility Length
    //public float invincLength = 1f;
    private float _invincCounter;
    public SpriteRenderer playerSprite;

    // Invicibility Flashing
    public bool flashInvincibility;
    public float flashLength;
    private float flashCounter;

    // Invicibility Fading
    public bool fadeInvincibility;
    public Color normalColor;
    public Color fadeColor;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInvincibility();
    }

    private void CheckInvincibility()
    {
        if (_invincCounter > 0)
        {
            _invincCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                playerSprite.enabled = !playerSprite.enabled;
                flashCounter = flashLength;
            }

            if (_invincCounter <= 0)
            {
                playerSprite.enabled = true;
                flashCounter = 0f;
                playerSprite.color = normalColor;
            }
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        //only allow damage to the plaer if the invic counter has completed countdown
        if (_invincCounter <= 0)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                LifeManager.instance.ReSpawn();
                AudioManager.instance.PlaySFX(7); // Player Death
            }
            else
            {
                _invincCounter = PlayerController.instance.stats.invincLength;
                playerSprite.color = fadeColor;

                //trigger knockback animation
                PlayerController.instance.KnockBack();
            }
            UpdateHealthUI();
            
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthUI();
        AudioManager.instance.PlaySFX(4); // Heal player
    }

    public void FullHealthRestore()
    {
        currentHealth = maxHealth;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthUI();
    }

    public void AddHearts(int heartsToAdd)
    {
        if (maxHealth < 20)
        {
            maxHealth = maxHealth + heartsToAdd;
            UpdateHealthUI();
        }
    }

    public void UpdateHealthUI()
    {
        UIManager.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }
}
