using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicPlayer : MonoBehaviour
{
    [SerializeField]
    private int trackToPlay;

    private void Start()
    {
        AudioManager.instance.PlayLevelMusic(trackToPlay);
    }
}
