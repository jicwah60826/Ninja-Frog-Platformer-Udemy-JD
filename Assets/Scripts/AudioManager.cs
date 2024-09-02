using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource menuMusic,
        bossMusic,
        levelCompleteMusic;
    public AudioSource[] levelTracks;
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
        if (instance == null)
        {
            SetupAudioManager();
        }
        // destroy instance if it not THIS one being setup
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetupAudioManager()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StopMusic()
    {
        menuMusic.Stop();
        bossMusic.Stop();
        levelCompleteMusic.Stop();

        foreach (AudioSource track in levelTracks)
        {
            track.Stop();
        }

        foreach (AudioSource track in soundEffects)
        {
            track.Stop();
        }
    }

    public void PlaySFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop(); // stop the sound if it is playing
        soundEffects[sfxNumber].Play(); // play the sound. allows playing sound in fast repetition
    }
    public void PlaySFXPitched(int sfxNumber)
    {

        soundEffects[sfxNumber].Stop(); // stop the sound if it is playing

        // Do a random pitch
        soundEffects[sfxNumber].pitch = Random.Range(.75f,1.25f);

        // Play the pitched sound
        soundEffects[sfxNumber].Play(); // play the sound. allows playing sound in fast repetition
    }

    public void StopSFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop();
    }

    public void PlayMenuMusic()
    {
        StopMusic();
        menuMusic.Play();
    }

    public void PlayBossMusic()
    {
        StopMusic();
        bossMusic.Play();
    }

    public void PlayLevelCompleteMusic()
    {
        StopMusic();
        levelCompleteMusic.Play();
    }

    public void PlayLevelMusic(int trackToPlay)
    {
        StopMusic();
        levelTracks[trackToPlay].Play();
    }
}
