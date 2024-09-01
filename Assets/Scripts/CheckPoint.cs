using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isActive;
    public Animator theAnim;

    [HideInInspector]
    public CheckpointManager cpManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isActive == false)
        {
            // de-ativate all OTHER checkpoins in scene and set this one as the active one
            cpManager.SetActiveCheckPoint(this);
            isActive = true;
            theAnim.SetBool("CheckPointActive", true);
            AudioManager.instance.PlaySFX(5); // Checkpoint sound
        }
    }

    public void DeactivateCheckPoint()
    {
        theAnim.SetBool("CheckPointActive", false);
        isActive = false;
    }
}
