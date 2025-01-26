using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// This script makes the face look in the direction of the mouse.
/// </summary>
public class Face : MonoBehaviour
{
    // describes the intensity of the rotation
    public float lookTilt = 0.5f;

    // Update is called once per frame
    void Update()
    {

        // calculate the target lookat point
        Vector3 lookTarg = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookTarg.z = 5;
        lookTarg *= lookTilt;
        lookTarg.z = -lookTarg.z;
        
        // make the face look at the target
        transform.rotation = Quaternion.LookRotation(-lookTarg);
    }
}
