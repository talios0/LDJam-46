using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Net;

public class LevelManager : MonoBehaviour
{
    public float delayBeforeNextLevel;
    private float delay;
    public float rotateSpeed;
    private bool delayComplete = false;

    public CameraController camController;
    public static bool disableRotation = false;

    private bool first = true;


    public Canvas LossCanvas; //The Losing Canvas
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

    public GameObject dropButton;
    public Animator dropButtonAnimator;


    private bool paused = false;
    private bool pausePossible = true;

    public GameObject[] gameOverButtons;

    private TutorialManager tutorials;


    public GameObject sphere;
    private void Start()
    {
        tutorials = GetComponent<TutorialManager>();
        LossCanvas = GameObject.Find("GameOverCanvas").GetComponent<Canvas>();
        LossCanvas.GetComponent<Canvas>().enabled = false;
        LevelComplete();
        ResetPositions();
        levelUI.text = level.ToString();
        tutorials = GetComponent<TutorialManager>();
    }

    private void FixedUpdate()
    {
        if (level == levels.Length) { currentLevel.transform.Rotate(new Vector3(0, 1, 0), rotateSpeed); return; }
        if (levelComplete)
        {
            if (!delayComplete) {
                currentLevel.transform.Rotate(new Vector3(0, 1, 0), rotateSpeed);
                delay += Time.deltaTime;
                if (delay >= delayBeforeNextLevel)
                {
                    delay = 0;
                    delayComplete = true;
                    LevelComplete();
                }
            }

            if (delayComplete && panComplete)
            {
                panComplete = false;
                LevelCompleteEnd();
            }
            else if (delayComplete && !panComplete)
            {
                PanCamera();
            }
        }
    }

    private void Update()
    {
        if (level == levels.Length) {
            if (paused) Resume();
            return;
        }
        if (Input.GetAxisRaw("Pause") != 0)
        {
            if (!pausePossible) return;
            pausePossible = false;
            if (!paused) Pause();
            else Resume();
        }
        else
        {
            pausePossible = true;
        }
    }

    public void LevelCompleteStart()
    {
        // Set position back to origin
        if (level+1 == levels.Length)
        {
            GameFinished();
            level++;
            return;
        }
        ResetPositions();
        levelComplete = true;
        panComplete = false;
        pausePossible = false;
        disableRotation = true;
    }

    private void LevelComplete()
    {
        // Check if at end
        if (first)
        {
            levelComplete = true;
            panComplete = true;
            LoadNextLevel();
            UpdateLevelUI();
            levelUIAnimator.Play("NewLevel");
            LevelCompleteEnd();
            first = false;
            return;
        }
        level++;
        UpdateLevelUI();
        levelUIAnimator.Play("NewLevel");
        // Load Next Level
        LoadNextLevel();
        camController.StartPanDown();
    }

    public void LevelCompleteEnd()
    {
        levelComplete = false;
        // Unload Previous
        UnloadLastLevel();

        tutorials.ShowTutorial(level);
        ShowDropButton();
        pausePossible = true;
        delayComplete = false;
        disableRotation = false;
    }

    private void LoadNextLevel()
    {
        newLevel = Instantiate(levels[level], newLevelLocation.position, Quaternion.identity);
        sphere = newLevel.GetComponentInChildren<SphereCollider>().gameObject;
        sphere.GetComponent<Rigidbody>().isKinematic = true;
        sphere.GetComponent<Rigidbody>().detectCollisions = false;
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
        panComplete = camController.PanDown();
    }

    public void UpdateLevelUI()
    {
        levelUI.text = level.ToString();
    }

    public void Restart()
    {
        if (LossCanvas.enabled)
        {
            LossCanvas.GetComponent<Animator>().Play("FadeOut");
            foreach (GameObject g in gameOverButtons)
            {
                g.GetComponent<Button>().enabled = false;
            }
        }
        LoadNextLevel();
        UnloadLastLevel();
        ResetPositions();
        dropButtonAnimator.Play("FadeIn");
        dropButtonAnimator.GetComponent<Button>().enabled = true;
        dropButtonAnimator.GetComponent<Button>().interactable = true;
        Resume();
    }

    public void Pause()
    {
        if (LossCanvas.enabled) return;
        paused = true;
        pauseUIAnimator.Play("FadeIn");
        levelUIAnimator.Play("FadeIn");
        restartButton.SetActive(true);
        resumeButton.SetActive(true);
        disableRotation = true;
        dropButton.GetComponent<Button>().enabled = false;
    }

    public void Resume()
    {
        paused = false;
        pauseUIAnimator.Play("FadeOut");
        levelUIAnimator.Play("FadeOut");
        restartButton.SetActive(false);
        resumeButton.SetActive(false);
        disableRotation = false;
        dropButton.GetComponent<Button>().enabled = true;
    }

    public Animator gameWinAnimator;

    private void GameFinished()
    {
        dropButton.SetActive(false);
        pausePossible = false;
        disableRotation = true;
        gameWinAnimator.Play("FadeIn");
    }

    public void StartOver()
    {
        if (LossCanvas.enabled)
        {
            LossCanvas.GetComponent<Animator>().Play("FadeOut");
            foreach (GameObject g in gameOverButtons)
            {
                g.GetComponent<Button>().interactable = false;
            }
        }
        sphere.GetComponent<playerScript>().SetUnlock(false);

        level = -1;
        LevelCompleteStart();
    }

    private void ShowDropButton()
    {
        dropButton.GetComponent<Button>().interactable = true;
        dropButtonAnimator.Play("FadeIn");
    }

    public void Drop()
    {
        sphere.GetComponent<Rigidbody>().isKinematic = false;
        sphere.GetComponent<Rigidbody>().detectCollisions = true;
        dropButtonAnimator.Play("FadeOut");
        dropButton.GetComponent<Button>().interactable = false;
        sphere.GetComponent<playerScript>().ballDropped = true;
    }

    public void quitGame()
    {
        Application.Quit();
    }
    public bool GameLoss()
    {
        disableRotation = true;
        pausePossible = false;
        if (levelComplete) return false;
        LossCanvas.enabled = true;
        LossCanvas.GetComponent<Animator>().Play("FadeIn");
        foreach (GameObject g in gameOverButtons)
        {
            g.GetComponent<Button>().interactable = true;
            g.GetComponent<Button>().enabled = true;
        }
        return true;
    }

    public int GetLevel() 
    {
        return level;
    }
}