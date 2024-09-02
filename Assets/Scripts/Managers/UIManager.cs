using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Animator theAnim;
    public Image[] heartIcons;
    public Sprite heartFull;
    public Sprite heartEmpty;

    public TMP_Text livesText;
    public TMP_Text fruitCountText;
    public GameObject gameOverScreen;

    public GameObject pauseScreen;

    public int mainMenuScene;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void UpdateHealthDisplay(int health, int maxHealth)
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = true;

            /* if (health <= i)
           {
               heartIcons[i].enabled = false;
           } */

            if (health > i)
            {
                heartIcons[i].sprite = heartFull;
            }
            else
            {
                heartIcons[i].sprite = heartEmpty;

                if (maxHealth <= i)
                {
                    heartIcons[i].enabled = false;
                }
            }
        }
    }

    public void UpdateLivesDisplay(int currentLives)
    {
        livesText.text = currentLives.ToString();
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    /*     public void MainMenu()
        {
            AudioManager.instance.PlayMenuMusic();
            SceneManager.LoadScene(0);
        } */

    public void UpdateFruitCountUI(int amount)
    {
        fruitCountText.text = amount.ToString();
    }

    public void AddLifeAnim()
    {
        theAnim.SetTrigger("AddLife");
    }

    public void PauseUnpause()
    {
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
        AudioManager.instance.PlayMenuMusic();
        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        PauseUnpause();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
