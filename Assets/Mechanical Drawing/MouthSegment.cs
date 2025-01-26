using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthSegment : MonoBehaviour
{

    // input in inspector, this value differs for each segment - indicates the X point along the curve that this segment represents
    public float xValue = 0;

    // determines the actual vertical scale of the mouth curve - the curve's value is mapped from 0-1 to 0-yScale
    public float yScale = 5;

    // curves that represent the shape of the mouth when it's open or closed
    public AnimationCurve mouthClosedCurve;
    public AnimationCurve mouthOpenCurve;

    // curve that the lerps follow
    public AnimationCurve lerpCurve;

    // minimum and maximum (mousey / screen.height) values for the mouth to start opening
    public float minMouthOpenY = 0.5f;
    public float maxMouthOpenY = 1f;

    // keeps track of the starting x position of this segment
    private float startX;

    public bool alwaysClosed; // set for those mouth segments that are always closed (they represent the top of the mouth when the mouth is open

    // the t value used in the transform localposition lerp in order to smooth out sudden transitions
    public float lerpSpeed = 0.1f;

    // the offset applied to the y value when frowning instead of smiling - we need to apply an offset because we want the center of the mouth to stay
    // in the same spot, not the edges
    public float frownYOffset = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        bool isMouseInScreen = true;

        Vector2 mousePos = Input.mousePosition;
        if (mousePos.x < 0 || mousePos.y < 0 || mousePos.x > Screen.width || mousePos.y > Screen.height) isMouseInScreen = false;

        Vector3 targPosition;

        if (alwaysClosed || !isMouseInScreen)
        {
            float targetYValue = -(1 - mouthClosedCurve.Evaluate(xValue));

            // turn the smile into a frown if the mouse is off screen
            if (!isMouseInScreen)
            {
                targetYValue *= -1;
                targetYValue += frownYOffset;
            }

            targPosition = new Vector3(startX, targetYValue, 0);
        } else
        {
            // get mouse position
            float mouseY = Input.mousePosition.y;
            // get ratio
            float ratio = Mathf.Clamp(1 - (mouseY / Screen.height), minMouthOpenY, maxMouthOpenY);
            // map it to between 0 and 1
            float mappedRatio = Mathf.Lerp(0, 1, Mathf.InverseLerp(minMouthOpenY, maxMouthOpenY, ratio));

            // lerp between closed and open curves by openness ratio
            targPosition = Vector3.down * (1 - Mathf.Lerp(mouthClosedCurve.Evaluate(xValue), mouthOpenCurve.Evaluate(xValue), lerpCurve.Evaluate(mappedRatio)));

            // make sure the new position retains our starting x position
            targPosition.x = startX;
        }

        // again, use lerp in a bit of a wonky way here in order to smooth out the transition from smile to frown instead of it being instant
        transform.localPosition = Vector3.Lerp(transform.localPosition, targPosition, lerpSpeed);

    }
}
