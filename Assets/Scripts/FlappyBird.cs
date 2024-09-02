using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D theRB;

    [SerializeField]
    private float flyForce;

    [SerializeField]
    private float startFlapAfter;

    [SerializeField]
    private float repeatFlapAfter;

    [SerializeField]
    private float gravityScale;

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("flappyBird", startFlapAfter, repeatFlapAfter);
    }

    private void flappyBird()
    {
        //theRB.gravityScale = gravityScale;
        //theRB.AddForce(Vector2.up * flyForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.instance.isGrounded = true;
            PlayerController.instance.Jump();
        }
    }
}
