﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float maxZoom;
    public float minZoom;
    public float disableWallsZoom;
    public float disableTopZoom;

    public float sensitivity;
    public float interpTime;
    public float snapAmount;

    private Vector3 zoomTo;


    private void Update()
    {
        if (LevelManager.disableRotation) return;
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            zoomTo = new Vector3(transform.position.x, transform.position.y, transform.position.z + Input.GetAxis("Mouse ScrollWheel") * -sensitivity * Time.deltaTime);
            zoomTo.z = Mathf.Clamp(zoomTo.z, minZoom, maxZoom);
        }
        if (zoomTo.z == 0) return;
        transform.position = Vector3.Lerp(transform.position, zoomTo, interpTime);
        if (Mathf.Abs(transform.position.z - zoomTo.z) < snapAmount) {
            transform.position = zoomTo;
            zoomTo.z = 0;
        }
    }

    public bool GetZoomDisable() {
        if (transform.position.z < disableWallsZoom)
        {
            return true;
        }
        return false;
    }

    public bool GetTopDisable() {
        if (transform.position.z < disableTopZoom) {
            return true;
        }
        return false;
    }
}
