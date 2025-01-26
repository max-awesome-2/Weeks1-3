using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class makes the face's eyes close whenever the mouse is held down.
/// </summary>
public class EyeBlink : MonoBehaviour
{

    public float blinkLerpSpeed = 0.05f;

    private Vector3 baseSize, blinkSize;

    private void Start()
    {
        baseSize = transform.localScale;
        blinkSize = baseSize;
        blinkSize.y = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {

        // use lerp a little bit incorrectly here in order make an over-time effect without using time.time
        if (Input.GetMouseButton(0))
        {
            transform.localScale = Vector3.Lerp(transform.localScale, blinkSize, blinkLerpSpeed);
        } else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, baseSize, blinkLerpSpeed);
        }
    }
}
