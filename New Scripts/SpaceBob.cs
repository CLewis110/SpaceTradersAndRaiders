using UnityEngine;
using System;
using System.Collections;

public class SpaceBob : MonoBehaviour
{
    float originalY;

    public float floatStrength; // You can change this in the Unity Editor to 
                                    // change the range of y positions that are possible.

    void Start()
    {
        floatStrength = 0.005f;   
    }

    void Update()
    {
        this.originalY = this.transform.position.y;

        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(Time.time) * floatStrength),
            transform.position.z);
    }
}
