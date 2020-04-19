using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject myCamera;

    public Transform endLocation;
    public float interpTime;
    private Vector3 endPos;

    public void StartPanDown() 
    {
        endPos = new Vector3(0, endLocation.position.y, myCamera.transform.position.z);
    }

    public void PanDown()
    {
        myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, endPos, interpTime);
    }
        
}
