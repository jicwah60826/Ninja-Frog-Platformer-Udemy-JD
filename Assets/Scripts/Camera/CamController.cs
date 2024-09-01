using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class CamController : MonoBehaviour
{
    private Vector3 _targetPoint = Vector3.zero;

    [Required("PlayerController required")]
    public PlayerController player;

    [Required("Assign Camera into theCam required")]
    public Camera theCam;

    [Title("Clamp Controls")]
    public bool clampPosition;

    [ColorPalette]
    public Color ClampGizmoColor;
    public Transform lowerLeftClamp;
    public Transform upperRightClamp;

    [Title("Move / Smooth Controls")]
    public float moveSpeed;

    public float lookAheadDistance;
    public float lookAheadSpeed;

    public bool smoothCameraY;

    private float _lookOffset;

    private bool _isFalling;

    [Title("Offset / Freeze Controls")]
    public float maxVertOffset;

    private float _halfWidth;
    private float _halfHeight;

    public bool freezeVertical,
        freezeHorizontal;
    private Vector3 _posistionStore;

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;

        player = FindFirstObjectByType<PlayerController>();

        _targetPoint = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            transform.position.z
        );

        _posistionStore = transform.position;
        // un-parent camera clmaps from camera transform
        lowerLeftClamp.SetParent(null);
        upperRightClamp.SetParent(null);

        _halfHeight = theCam.orthographicSize;
        _halfWidth = theCam.orthographicSize * theCam.aspect;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // basic movement
        //targetPoint.x = player.transform.position.x; // now set by lookAhead and lookOffset below

        if (!smoothCameraY)
        {
            _targetPoint.y = player.transform.position.y;
        }
        else if (smoothCameraY)
        {
            if (player.isGrounded)
            {
                _targetPoint.y = player.transform.position.y;
            }
        }

        if (transform.position.y - player.transform.position.y > maxVertOffset)
        {
            _isFalling = true;
        }

        if (_isFalling)
        {
            _targetPoint.y = player.transform.position.y;

            if (player.isGrounded)
            {
                _isFalling = false;
            }
        }

        // moving right
        if (player.theRB.velocity.x > 0f)
        {
            _lookOffset = Mathf.Lerp(_lookOffset, lookAheadDistance, lookAheadSpeed * Time.deltaTime);
        }

        // moving left
        if (player.theRB.velocity.x < 0f)
        {
            _lookOffset = Mathf.Lerp(
                _lookOffset,
                -lookAheadDistance,
                lookAheadSpeed * Time.deltaTime
            );
        }

        // move the target point
        _targetPoint.x = player.transform.position.x + _lookOffset;

        // move the camera
        transform.position = Vector3.Lerp(
            transform.position,
            _targetPoint,
            moveSpeed * Time.deltaTime
        );

        if (freezeVertical)
        {
            transform.position = new Vector3(
                transform.position.x,
                _posistionStore.y,
                transform.position.z
            );
        }
        if (freezeHorizontal)
        {
            transform.position = new Vector3(
                _posistionStore.x,
                transform.position.y,
                transform.position.z
            );
        }

        if (clampPosition)
        {
            transform.position = new Vector3(
                Mathf.Clamp(
                    transform.position.x,
                    lowerLeftClamp.position.x + _halfWidth,
                    upperRightClamp.position.x - _halfWidth
                ),
                Mathf.Clamp(
                    transform.position.y,
                    lowerLeftClamp.position.y + _halfHeight,
                    upperRightClamp.position.y - _halfHeight
                ),
                transform.position.z
            );
        }
    }

    private void OnDrawGizmos()
    {
        if (clampPosition)
        {
            Gizmos.color = ClampGizmoColor;

            // Draw line from lower left corner going UP
            Gizmos.DrawLine(
                lowerLeftClamp.position,
                new Vector3(lowerLeftClamp.position.x, upperRightClamp.position.y, 0f)
            );

            // Draw line from lower left corner going RIGHT
            Gizmos.DrawLine(
                lowerLeftClamp.position,
                new Vector3(upperRightClamp.position.x, lowerLeftClamp.position.y, 0f)
            );

            // Draw line from upper right corner going LEFT
            Gizmos.DrawLine(
                upperRightClamp.position,
                new Vector3(lowerLeftClamp.position.x, upperRightClamp.position.y, 0f)
            );

            // Draw line from upper right corner going DOWN
            Gizmos.DrawLine(
                upperRightClamp.position,
                new Vector3(upperRightClamp.position.x, lowerLeftClamp.position.y, 0f)
            );
        }
    }
}
