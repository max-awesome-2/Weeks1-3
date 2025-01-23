using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolManager : MonoBehaviour
{
    public Transform patroller;

    public List<Transform> points;
    public GameObject pointPrefab;

    public float navTime = 1;
    private int currentPointIndex;
    private float navTimer = 0;
    bool reachedTarget = true;

    Transform fromTarget, nextTarget;

    public AnimationCurve moveCurve;

    // Start is called before the first frame update
    void Start()
    {
        Transform startPoint = Instantiate(pointPrefab).transform;
        startPoint.position = Vector3.zero;
        points.Add(startPoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            Transform newPoint = Instantiate(pointPrefab).transform;
            newPoint.position = pos;

            points.Add(newPoint.transform);
            
        }

        // travel to next waypoint
        if (currentPointIndex < points.Count)
        {

            // set current and nwxt waypoint if they exist
            if (fromTarget == null && currentPointIndex < points.Count)
            {
                fromTarget = points[currentPointIndex];
                print("set from target: " + fromTarget);
            }
            if (nextTarget == null && currentPointIndex + 1 < points.Count)
            {
                nextTarget = points[currentPointIndex + 1];
                print("set to target: " + nextTarget);

            }

            // if current and target waypoints do not both exist, wait until they do
            if (fromTarget == null || nextTarget == null) return;

            // once we select a valid next target, reset the travel timer
            if (reachedTarget)
            {
                reachedTarget = false;
                navTimer = Time.time;
            }

            // travel from 
            float tValue = 0;
            if (Time.time - navTimer > navTime)
            {
                currentPointIndex++;
                reachedTarget = true;
                patroller.position = nextTarget.position;
                fromTarget = null;
                nextTarget = null;
                return;
            } else
            {
                tValue = (Time.time - navTimer) / navTime;
            }

            //print("fromtarg: " + fromTarget + ", " + nextTarget);
            patroller.position = Vector3.Lerp(fromTarget.position, nextTarget.position, moveCurve.Evaluate(tValue));

        }
    }
}
