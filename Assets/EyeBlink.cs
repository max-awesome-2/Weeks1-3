using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class makes the face's eyes blink every however many seconds.
/// </summary>
public class EyeBlink : MonoBehaviour
{

    // number of seconds between blinks
    public float blinkDelay = 5f;

    // total time for one blink
    public float blinkTime = 0.5f;

    private float baseSize;

    public AnimationCurve blinkCurve;


    // Update is called once per frame
    void Update()
    {
        float blinkProgress = 1;
        float timeMod = Time.time % blinkDelay;


        if (timeMod > (blinkDelay - blinkTime))
        {
            // calculate the progress through the blink curve
            blinkProgress = 1 - (blinkDelay - timeMod) / blinkTime;

            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.x * blinkCurve.Evaluate(blinkProgress), transform.localScale.z);
        }
    }
}
