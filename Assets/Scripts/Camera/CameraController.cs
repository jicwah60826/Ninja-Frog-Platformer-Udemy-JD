using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public bool freezeVertical,
        freezeHorizontal;
    private Vector3 posistionStore;

    public bool clampPosition;
    public Transform clampMin;
    public Transform clampMax;

    private float halfWidth;
    private float halfHeight;

    public Camera theCam;

    [SerializeField]
    private Vector3 offset = new Vector3(0f, 0f, -10f);

    [SerializeField]
    private float smoothTime = 0.25f;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        posistionStore = transform.position;
        // un-parent camera clmaps from camera transform
        clampMin.SetParent(null);
        clampMax.SetParent(null);

        halfHeight = theCam.orthographicSize;
        halfWidth = theCam.orthographicSize * theCam.aspect;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );

        if (freezeVertical)
        {
            transform.position = new Vector3(
                transform.position.x,
                posistionStore.y,
                transform.position.z
            );
        }
        if (freezeHorizontal)
        {
            transform.position = new Vector3(
                posistionStore.x,
                transform.position.y,
                transform.position.z
            );
        }

        if (clampPosition)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth),
                Mathf.Clamp(transform.position.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight),
                transform.position.z
            );
        }
    }

    private void OnDrawGizmos() {
        if(clampPosition){
            Gizmos.color = Color.cyan;

            // Draw line from lower left corner going UP
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMin.position.x,clampMax.position.y,0f));

            // Draw line from lower left corner going RIGHT
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMax.position.x,clampMin.position.y,0f));

            // Draw line from upper right corner going LEFT
            Gizmos.DrawLine(clampMax.position, new Vector3(clampMin.position.x,clampMax.position.y,0f));

            // Draw line from upper right corner going DOWN
            Gizmos.DrawLine(clampMax.position, new Vector3(clampMax.position.x,clampMin.position.y,0f));
        }
        
    }
}
