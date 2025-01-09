using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScript : MonoBehaviour
{



    float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        Vector3 pixelPos = Camera.main.WorldToScreenPoint(transform.position);

        if (pixelPos.x < 0 || pixelPos.x > Screen.width)
        {
            speed *= -1;
           
        }
    }
}
