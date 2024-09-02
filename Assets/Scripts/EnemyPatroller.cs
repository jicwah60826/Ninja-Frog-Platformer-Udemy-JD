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

    private Animator anim;

    private bool isMoving;

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

        // get animator component
        anim = GetComponent<Animator>();

        isMoving = true;
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
            isMoving = false;

            if (waitCounter <= 0)
            {
                // move to the next point and reset timer
                currentPoint++;

                // if we reach the last point the array, set back to zero to move back
                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }

                waitCounter = timeAtPoints;
                isMoving = true;

                // flip - if on the LEFT of the next patrol point - face right
                if (transform.position.x < patrolPoints[currentPoint].position.x)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else
                {
                    transform.localScale = Vector3.one;
                }
            }
        }

        // animations
        anim.SetBool("isMoving", isMoving);
    }
}
