using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public CheckPoint[] allCheckPoints;

    private CheckPoint _activeCP;

    public Vector3 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Find all checkpoints in the scene but do not sort them in the array
        allCheckPoints = FindObjectsByType<CheckPoint>(FindObjectsSortMode.None);

        foreach (CheckPoint checkPoint in allCheckPoints)
        {
            // for all checkpoints: auto-assign this script into the cpManager slot in the editor
            checkPoint.cpManager = this;
        }

        respawnPosition = PlayerController.instance.transform.position;
    }

    public void DeactivateAllCheckPoints()
    {
        foreach (CheckPoint checkPoint in allCheckPoints)
        {
            checkPoint.DeactivateCheckPoint();
        }
    }

    public void SetActiveCheckPoint(CheckPoint newActiveCP)
    {
        DeactivateAllCheckPoints();
        _activeCP = newActiveCP;

        // set the respawnPosition as the position of the now active checkpoint
        respawnPosition = newActiveCP.transform.position;
    }
}
