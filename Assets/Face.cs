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

    public float lerpSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {

        // calculate the target lookat point
        Vector3 lookTarg = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookTarg.z = 5;
        lookTarg *= lookTilt;
        lookTarg.z = -lookTarg.z;

        // check if the mouse is inside the screen - if not, set the look target to the center of the screen
        Vector2 mousePos = Input.mousePosition;
        if (mousePos.x < 0 || mousePos.y < 0 || mousePos.x > Screen.width || mousePos.y > Screen.height)
        {
            lookTarg = new Vector3(0, 0, -10);
        }

        // make the face look at the target - again, use weird lerp to make this an over-time effect without actually referencing time variables
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-lookTarg), lerpSpeed);
    }
}
