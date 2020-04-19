using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFunctionality : MonoBehaviour
{
    public void EnableCanvas() {
        GetComponent<Canvas>().enabled = true;
    }
    public void DisableCanvas()
    {
        GetComponent<Canvas>().enabled = false;
    }
}
