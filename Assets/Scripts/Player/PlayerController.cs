using System.Collections;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor.StateUpdaters;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField]
    public PlayerStats stats;
    public Rigidbody2D theRB;
    private float _activeSpeed;

    // Grounded & Jump vars
    public bool isGrounded;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    private int _additionalJumps;

    private float knockbackCounter;

    private bool _isKnockingBack;

    public Animator anim;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        KnockBackCheck();
        RunCheck();
        Move();
        TurnCheck();
        JumpCheck();
        PlayerAnimations();
        DevCommands();
    }

    private void DevCommands()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("You hot the J key");
        }
    }

    private void KnockBackCheck()
    {
        // Begin Knockback timer
        knockbackCounter -= Time.deltaTime;
        if (knockbackCounter <= 0)
        {
            // make sure knockback counter does not go below zero
            knockbackCounter = 0;
            _isKnockingBack = false;
        }
    }

    private void GroundCheck()
    {
        // Determine if grounded
        isGrounded = Physics2D.OverlapCircle(
            groundCheckPoint.position,
            groundCheckRadius,
            whatIsGround
        );

        //reset jump count from stats SO when player has touched the ground
        if (isGrounded)
        {
            _additionalJumps = stats.additionalJumps;
        }
    }

    private void JumpCheck()
    {
        //get jumps

        if (_isKnockingBack == false)
        {
            // Jump Check
            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    Jump();
                }
                else if (_additionalJumps > 0)
                {
                    DoubleJump();
                }
            }
        }
    }

    private void DoubleJump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, stats.jumpForce);
        _additionalJumps -= 1;
        anim.SetTrigger("doDoubleJump");
        AudioManager.instance.PlaySFX(18); // Jump Sound
    }

    public void Jump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, stats.jumpForce);
        AudioManager.instance.PlaySFXPitched(0); // Jump Sound
    }

    private void TurnCheck()
    {
        if (!_isKnockingBack)
        {
            //handle direction change
            if (theRB.velocity.x < 0)
            {
                // facing left
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (theRB.velocity.x > 0)
            {
                // facing right
                transform.localScale = Vector3.one;
            }
        }
    }

    private void RunCheck()
    {
        // Run Check
        _activeSpeed = stats.moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            //Set Active speed to runSpeed
            _activeSpeed = stats.runSpeed;
        }
    }

    public void KnockBack()
    {
        theRB.velocity = new Vector2(0f, stats.jumpForce * .5f);
        anim.SetTrigger("isKnockingBack");
        knockbackCounter = stats.knockbackLength;
        _isKnockingBack = true;
    }

    private void Move()
    {
        if (!_isKnockingBack)
        {
            // Move Player
            theRB.velocity = new Vector2(
                Input.GetAxisRaw("Horizontal") * _activeSpeed,
                theRB.velocity.y
            );
        }
        else
        {
            knockbackCounter -= Time.deltaTime;
            theRB.velocity = new Vector2(
                stats.knockbackForce * -transform.localScale.x,
                theRB.velocity.y
            );
        }
    }

    private void PlayerAnimations()
    {
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", theRB.velocity.y);
    }
}
