using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

public class ParallaxBackground : MonoBehaviour
{
    private Transform _theCam;

    [Range(0f, 1f)]
    public float parallaxSpeed;

    public Transform bg;

    public Transform far;

    public Transform mid;

    // Start is called before the first frame update
    void Start()
    {
        _theCam = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        bg.position = new Vector3(
            _theCam.position.x * parallaxSpeed,
            bg.position.y * parallaxSpeed,
            bg.position.z
        );

        far.position = new Vector3(
            _theCam.position.x * (parallaxSpeed * .5f),
            far.position.y * (parallaxSpeed * .5f),
            far.position.z
        );

        mid.position = new Vector3(
            _theCam.position.x * (parallaxSpeed * .25f),
            mid.position.y * (parallaxSpeed * .25f),
            mid.position.z
        );
    }
}
