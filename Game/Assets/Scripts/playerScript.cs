using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class playerScript: MonoBehaviour
{
    public LevelManager levelManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "laser")
        {
            // You Lose
            levelManager.GameLoss();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "WinPlat")
        {
            // You Win
            levelManager.LevelCompleteStart();
        }
    }
}
