using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam;
    public float parallaxEffect;

    private float length, startPos;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffect);
        float distance = cam.transform.position.x * parallaxEffect;
       
        transform.position = new Vector2(startPos + distance, transform.position.y);

        if(temp > startPos + length){
            startPos += length;
        }else if(temp < startPos - length){
            startPos -= length;
        }
    }
}
