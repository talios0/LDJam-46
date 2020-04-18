using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject[] levels;

    public int level;

    public Transform newLevelLocation;
    public GameObject currentLevel;
    private GameObject newLevel;

    private bool levelComplete;
    public bool panComplete;

    private void Start()
    {
        LevelCompleteStart();
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

    public void LevelCompleteStart()
    {
        levelComplete = true;
        panComplete = false;
        // Check if at end
        level++;
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
        // Pause
        Pause();
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

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    private void GameFinished()
    {
        // Load new Screen
    }
}
