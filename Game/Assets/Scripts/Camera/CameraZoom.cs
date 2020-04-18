using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float maxZoom;
    public float minZoom;

    public float sensitivity;
    public float interpTime;
    public float snapAmount;

    private Vector3 zoomTo;

    private void FixedUpdate()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            zoomTo = new Vector3(transform.position.x, transform.position.y, transform.position.z + Input.GetAxis("Mouse ScrollWheel") * -sensitivity);
            zoomTo.z = Mathf.Clamp(zoomTo.z, minZoom, maxZoom);
        }
        if (zoomTo.z == 0) return;
        transform.position = Vector3.Lerp(transform.position, zoomTo, interpTime);
        if (Mathf.Abs(transform.position.z - zoomTo.z) < snapAmount) {
            transform.position = zoomTo;
            zoomTo.z = 0;
        }
    }
}
