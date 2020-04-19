using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float panSpeed;
    public Vector3 startPos;
    public Vector3 endPos;

    public float speed = 1.0f;
    public GameObject myCamera;
    private float startTime;
    private float journeyLength;
    public float camSpeed;
    bool isMoving = true;
    // Start is called before the first frame update
    void Start()
    {
        // myCamera.transform.position = new Vector3(0.0f, 0.0f, -10.0f);
        startPos = myCamera.transform.position;
        endPos = new Vector3(0.0f, -20.0f, 14.0f);
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPos, endPos);
    }
    private void FixedUpdate()
    {
        Vector3 lastPosition = myCamera.transform.position;
        Vector3 currentPosition = myCamera.transform.position;
           
    }
    public void panDown()
    {



        float distCovered = (Time.time - startTime) * speed;

        float fractionOfJOurney = distCovered / journeyLength;

        myCamera.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJOurney);
    }
        
}
