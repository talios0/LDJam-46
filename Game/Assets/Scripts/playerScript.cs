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
    private bool lose;

    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser")
        {
            // You Lose
            levelManager.GameLoss();
            lose = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (lose) return;
        if (collision.gameObject.tag == "WinPlat")
        {
            // You Win
            levelManager.LevelCompleteStart();
        }
    }
}
