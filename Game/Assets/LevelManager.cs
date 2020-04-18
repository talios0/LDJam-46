using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.InteropServices;

public class LevelManager : MonoBehaviour
{
    public static bool disableRotation = false;

    private Canvas LossCanvas; //The Losing Canvas
    public GameObject[] levels;

    public int level;

    public Transform newLevelLocation;
    public GameObject currentLevel;
    private GameObject newLevel;

    private bool levelComplete;
    public bool panComplete;

    public TextMeshProUGUI levelUI;
    public Animator levelUIAnimator;
    public Animator pauseUIAnimator;
    public GameObject resumeButton;
    public GameObject restartButton;


    private bool paused = false;
    private bool pausePossible = true;
    private void Start()
    {
        LossCanvas = GameObject.Find("GameOverCanvas").GetComponent<Canvas>();
        LossCanvas.GetComponent<Canvas>().enabled = false;
        LevelCompleteStart();
        levelUI.text = level.ToString();
    }

    private void FixedUpdate()
    {
        if (levelComplete)
        {
            if (panComplete)
            {
                panComplete = false;
                LevelCompleteEnd();
            }
            else
            {
                PanCamera();
            }
        }
    }
    private void Update()
    {
        if (Input.GetAxisRaw("Pause") != 0)
        {
            if (!pausePossible) return;
            pausePossible = false;
            if (!paused) Pause();
            else Resume();
        }
        else {
            pausePossible = true;
        }
    }

    public void LevelCompleteStart()
    {
        disableRotation = true;
        levelComplete = true;
        panComplete = false;
        // Check if at end
        level++;
        UpdateLevelUI();
        levelUIAnimator.Play("NewLevel");
        if (level > levels.Length)
        {
            GameFinished();
        }
        // Load Next Level
        LoadNextLevel();
    }

    public void LevelCompleteEnd()
    {
        levelComplete = false;
        // Unload Previous
        UnloadLastLevel();
        // Set position back to origin
        ResetPositions();
    }

    private void LoadNextLevel()
    {
        newLevel = Instantiate(levels[level], newLevelLocation.position, Quaternion.identity);
    }

    private void ResetPositions()
    {
        currentLevel.transform.position = new Vector3(0, 0, -9);
        Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
    }

    private void UnloadLastLevel()
    {
        Destroy(currentLevel);
        currentLevel = newLevel;
        newLevel = null;
    }

    private void PanCamera()
    {

    }

    public void UpdateLevelUI() {
        levelUI.text = level.ToString();
    }

    public void Restart() {
        LoadNextLevel();
        UnloadLastLevel();
        ResetPositions();
        Resume();
    }

    public void Pause()
    {
        paused = true;
        pauseUIAnimator.Play("FadeIn");
        levelUIAnimator.Play("FadeIn");
        restartButton.SetActive(true);
        resumeButton.SetActive(true);
        disableRotation = true;
    }

    public void Resume()
    {
        paused = false;
        pauseUIAnimator.Play("FadeOut");
        levelUIAnimator.Play("NewLevel");
        restartButton.SetActive(false);
        resumeButton.SetActive(false);
        disableRotation = false;
    }

    private void GameFinished()
    {
        // Load new Screen
    }
    public void loadLevel(string levelName) {
        try
        {
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
            Debug.Log("Load Scene " + levelName);
        }
        catch {
            Debug.Log("Level Load Not Valid");
        }
    }
    public void quitGame() {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
    public void GameLoss()
    {
        LossCanvas.enabled = true;
    }
}
