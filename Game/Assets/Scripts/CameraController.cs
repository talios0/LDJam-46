using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 position;
    [SerializeField] float panSpeed;

    public GameObject myCamera;
    // Start is called before the first frame update
    void Start()
    {
        position = new Vector3(0.0f, 0.0f, -10.0f);
        myCamera.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            panDown();
        }
    }
    void panDown()
    {
        myCamera.transform.position += new Vector3(0.0f, -20.0f, 0.0f);
    }
}

