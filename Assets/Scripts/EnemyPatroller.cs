using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    // define array for all patrol points
    public Transform[] patrolPoints;

    // keep track of where patroller is at
    private int currentPoint;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float timeAtPoints;

    private float waitCounter;

    // Start is called before the first frame update
    void Start()
    {
        // unparent all patrol points so they are static and do not move with the root object
        foreach (var t in patrolPoints)
        {
            t.SetParent(null);
        }

        // intialize timer
        waitCounter = timeAtPoints;
    }

    // Update is called once per frame
    void Update()
    {
        // move towards the 1st patrol point
        transform.position = Vector3.MoveTowards(
            transform.position,
            patrolPoints[currentPoint].position,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < .001)
        {
            // start counting down
            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                // move to the next point and reset timer
                currentPoint++;
                waitCounter = timeAtPoints;
            }

            // if we reach the last point the array, set back to zero to move back
            if (currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0;
            }
        }
    }
}
