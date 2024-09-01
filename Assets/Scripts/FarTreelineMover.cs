using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarTreelineMover : MonoBehaviour
{
    public float maxDistance; // how far an object should be off cam before we move it

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        float distance = transform.position.x - Camera.main.transform.position.x;

        if (distance > maxDistance)
        {
            //move the object 2 distances from where it's at right now.
            transform.position -= new Vector3(maxDistance * 2f, 0f, 0f);
        }
        if (distance < -maxDistance)
        {
            //move the object 2 distances from where it's at right now.
            transform.position += new Vector3(maxDistance * 2f, 0f, 0f);
        }
    }
}
