using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class playerScript: MonoBehaviour
{
    public GameObject levelManageAccess; 
    int playerLevel;
    public GameObject cameraScriptAccess;
    CameraController a;
    LevelManager b;
    void Start()
    {
        a = cameraScriptAccess.GetComponent<CameraController>();
        b = levelManageAccess.GetComponent<LevelManager>();
        playerLevel = 0;
        transform.position = new Vector3(-0.225f, -4.16f, -11.41f);
        // transform.position = new Vector3(-6.57f, 6.54f, -15.34f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            Debug.Log("Collision Between Laser and Ball Detected");
            SceneManager.LoadScene("LoseScreen", LoadSceneMode.Single);
            b.GameLoss(); //Displays Loss Canvas
        }
        if (collision.gameObject.tag == "WinPlat")
        {//This event should increment the score and move onto the next cube
            Debug.Log("Collision with a win platform detected");
            b.LevelCompleteStart();
            //a.panDown();
        }
       
    }
}
