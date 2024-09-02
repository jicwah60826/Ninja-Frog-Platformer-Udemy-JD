using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMLoader : MonoBehaviour
{
    public AudioManager theAM;

    private void Awake()
    {
        if (AudioManager.instance == null)
        {
            /* If audio manager doesn't exist, instantiate the audio manager into the scene and immediately call the SetupAudioManager function */
            Instantiate(theAM).SetupAudioManager();
        }
    }
}
