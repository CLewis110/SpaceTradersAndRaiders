using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float panSpeed = 20f;
    private float panBorder = 10f;
    private float panAdd = 0;

    private float zoomMin = 20;
    private float zoomMax = 60;
    private float zoomSpeed;

    public Camera cam;

    private void Start()
    {
        cam.orthographicSize = 30;
        zoomSpeed = 20;
    }
    void Update()
    {

        // W,A,S,D zoom  
        if(Input.GetKey("w"))
        {
            transform.Translate(Vector3.up * (panSpeed + panAdd) * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.down * (panSpeed + panAdd) * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * (panSpeed + panAdd) * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * (panSpeed + panAdd) * Time.deltaTime, Space.World);
        }


        //FIX using camera SIZE instead of position
        
        if(Input.GetKey("q"))
        {
            cam.orthographicSize = cam.orthographicSize + zoomSpeed * Time.deltaTime;
            panAdd++;
            if(panAdd >= 30)
            {
                panAdd = 30;
            }

            if(cam.orthographicSize >= zoomMax)
            {
                cam.orthographicSize = zoomMax;

            }
        }

        if (Input.GetKey("e"))
        {
            cam.orthographicSize = cam.orthographicSize - zoomSpeed * Time.deltaTime;
            panAdd--;
            if(panAdd <= 0)
            {
                panAdd = 0;
            }

            if (cam.orthographicSize <= zoomMin)
            {
                cam.orthographicSize = zoomMin;
            }
        }



    }


}
