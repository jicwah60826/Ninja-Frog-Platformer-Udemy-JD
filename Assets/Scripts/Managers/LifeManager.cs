using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;
    public PlayerStats stats;
    public float waitToRespawn;
    public int currentLives;
    public int extraLifeThreshold;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateUILivesDisplay();
    }

    public void ReSpawn()
    {
        // instantiate death effect
        Instantiate(
            PlayerHealthController.instance.deathEffect,
            PlayerController.instance.transform.position,
            PlayerController.instance.transform.rotation
        );
        PlayerController.instance.gameObject.SetActive(false);
        currentLives--;
        Debug.Log("ReSpawn called - currentLives = " + currentLives);

        if (currentLives > 0)
        {
            StartCoroutine(ReSpawnCo());
            Debug.Log("currentLives count: " + currentLives);
        }
        else
        {
            currentLives = 0;
            StartCoroutine(GameOverCo());
        }

        if (UIManager.instance != null)
        {
            UpdateUILivesDisplay();
        }
    }

    public IEnumerator ReSpawnCo()
    {
        yield return new WaitForSeconds(waitToRespawn);

        //Set player to last checkpoint position
        PlayerController.instance.transform.position = FindFirstObjectByType<CheckpointManager>().respawnPosition;

        //restore player health
        PlayerHealthController.instance.FullHealthRestore();
        PlayerController.instance.gameObject.SetActive(true);
        // do player respawn effect
        Instantiate(
            PlayerHealthController.instance.respawnEffect,
            PlayerController.instance.transform.position,
            PlayerController.instance.transform.rotation
        );
        AudioManager.instance.PlaySFX(16); // Player respawn
    }

    public IEnumerator GameOverCo()
    {
        yield return new WaitForSeconds(waitToRespawn);

        if (UIManager.instance != null)
        {
            //show game over screen
            UIManager.instance.ShowGameOverScreen();
            ResetStats();
            Debug.Log("Game over baby!");
        }
    }

    public void AddLifeCollectible(int amount)
    {
        currentLives += amount;
        UpdateUILivesDisplay();
    }

    private void ResetStats()
    {
        // reset stats
        stats.level = 1;
        stats.moveSpeed = 8;
        stats.runSpeed = 16f;
        stats.jumpForce = 18f;
        stats.additionalJumps = 0;
        stats.healthReChargeRate = .25f;
        stats.knockbackLength = 1;
        stats.knockbackForce = 5;
        stats.invincLength = 1;
    }

    public void AddLife()
    {
        currentLives++;
        AudioManager.instance.PlaySFX(2); // Add life sound
        UpdateUILivesDisplay();
        UIManager.instance.AddLifeAnim();
    }

    public void UpdateUILivesDisplay()
    {
        UIManager.instance.UpdateLivesDisplay(currentLives);
    }
}
