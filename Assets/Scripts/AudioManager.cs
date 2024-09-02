using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource level1;
    public AudioSource level2;
    public AudioSource levelSelect;
    public AudioSource levelVictory;
    public AudioSource mainMenu;
    public AudioSource bossBattle;
    public AudioSource winScreen;
    public AudioSource[] soundEffects;

    /*
        0: Player Jump
        1: Player Hurt
        2: Extra Life
        3: Fruit Pickup
        4: Get Health
        5: Checkpoint
        6: Trampoline
        7: Player Death
        8: Player Exit level
        9:Boss Death
        10: Boss Impact
        11: Boss Shot
        12: Enemy Block
        13: Enemy Explode
        14: Enemy Hit
        15: Enemy Plant Shoot
        16: Player Respawn
        17: Hero Jump Hollow Knight
        18: Double Jump Hollow Knight
        19: Hero Damage Hollow Knight
        20: Enemy Explode
    */

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void PlayMainMenuMusic()
    {
        mainMenu.Stop();
        mainMenu.Play();
    }

    public void PlayLevel1Music()
    {
        level1.Stop();
        level1.Play();
    }

    public void PlayLevel2Music()
    {
        level2.Stop();
        level2.Play();
    }

    public void PlayLevelSelectMusic()
    {
        levelSelect.Stop();
        levelSelect.Play();
    }

    public void PlayLevelVictoryMusic()
    {
        levelVictory.Stop();
        levelVictory.Play();
    }

    public void PlayWinScreenMusic()
    {
        winScreen.Stop();
        winScreen.Play();
    }

    public void PlaySFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop(); // stop the sound if it is playing
        soundEffects[sfxNumber].Play(); // play the sound. allows playing sound in fast repetition
    }

    public void StopSFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop();
    }

    public void PlayBossBattle()
    {
        bossBattle.Stop();
        bossBattle.Play();
    }
}
