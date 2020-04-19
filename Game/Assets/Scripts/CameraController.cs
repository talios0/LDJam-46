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

    public bool PanDown()
    {
        myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, endPos, interpTime);
        if (Mathf.Abs(myCamera.transform.position.y - endPos.y) < 0.15f) return true;
        return false;
    }

}