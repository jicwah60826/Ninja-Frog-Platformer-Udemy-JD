using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject levelEndEffect;

    [SerializeField]
    private GameObject effectTarget;

    [SerializeField]
    private bool isEnding;

    [SerializeField]
    private string nextLevel;

    [SerializeField]
    private float waitToEndLevel;

    [SerializeField]
    private GameObject blocker;

    [SerializeField]
    private float fadeTime;

    private void Start()
    {
        blocker.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnding)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Level Completed!");
                anim.SetTrigger("LevelCompleted");
                isEnding = true;
                AudioManager.instance.PlayLevelCompleteMusic();
                blocker.SetActive(true);
                Instantiate(
                    levelEndEffect,
                    effectTarget.transform.position,
                    effectTarget.transform.rotation
                );
                StartCoroutine(EndLevelCo());
            }
        }
    }

    IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(waitToEndLevel - fadeTime);
        Debug.Log("Level is ending");

        // Start fading at the difference between waitToEndLevel & fadeTime
        UIManager.instance.FadeToBlack();

        // Wait for the fade to finish
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(nextLevel);
    }
}
