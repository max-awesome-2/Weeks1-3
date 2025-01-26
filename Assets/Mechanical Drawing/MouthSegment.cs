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

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
       

        Vector3 targPosition;

        if (alwaysClosed)
        {
            targPosition = new Vector3(startX, -(1 - mouthClosedCurve.Evaluate(xValue)), 0);
        } else
        {
            // get mouse position
            float mouseY = Input.mousePosition.y;
            // get ratio
            float ratio = Mathf.Clamp(1 - (mouseY / Screen.height), minMouthOpenY, maxMouthOpenY);
            // map it to between 0 and 1
            float mappedRatio = Mathf.Lerp(0, 1, Mathf.InverseLerp(minMouthOpenY, maxMouthOpenY, ratio));

            // lerp between closed and open curves by openness ratio
            targPosition = Vector3.down * (1 - Mathf.Lerp(mouthClosedCurve.Evaluate(xValue), mouthOpenCurve.Evaluate(xValue), mappedRatio));

            // make sure the new position retains our starting x position
            targPosition.x = startX;
        }

        transform.localPosition = targPosition;


    }
}
