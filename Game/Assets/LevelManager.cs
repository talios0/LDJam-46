using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LevelManager : MonoBehaviour
{
    public static bool disableRotation = false;


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

    private void Start()
    {
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
        pauseUIAnimator.Play("FadeOut");
        restartButton.SetActive(false);
        resumeButton.SetActive(false);
        disableRotation = false;
        LoadNextLevel();
        UnloadLastLevel();
        ResetPositions();
    }

    public void Pause()
    {
        pauseUIAnimator.Play("FadeIn");
        restartButton.SetActive(true);
        resumeButton.SetActive(true);
        disableRotation = true;
    }

    public void Resume()
    {
        pauseUIAnimator.Play("FadeOut");
        restartButton.SetActive(false);
        resumeButton.SetActive(false);
        disableRotation = false;
    }

    private void GameFinished()
    {
        // Load new Screen
    }
}
