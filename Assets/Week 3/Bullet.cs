using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    public bool hasBeenFired = false;

    public AnimationCurve curve;
    public float cycleSpeed = 1;

    void Update()
    {
        if (!hasBeenFired)
        {
            PointAtMouse();
        }
        else
        {
            Movement();
        }

    }

    void PointAtMouse()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;
        Vector2 direction = mouse - transform.position;

        transform.up = direction;
    }

    void Movement()
    {
        print("move: " + (transform.up * speed * Time.deltaTime * curve.Evaluate(Time.time * cycleSpeed)));
        transform.position += transform.up * speed * Time.deltaTime * curve.Evaluate(Time.time * cycleSpeed);
    }
}
