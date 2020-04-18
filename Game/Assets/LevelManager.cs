using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static bool disableRotation = false;

    private bool first = true;


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


    public GameObject sphere;
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
        levelComplete = true;
        panComplete = false;
        // Check if at end
        if (first) {
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
        if (level > levels.Length)
        {
            GameFinished();
        }
        // Load Next Level
        LoadNextLevel();
    }

    public void LevelCompleteEnd()
    {
        sphere = newLevel.GetComponentInChildren<SphereCollider>().gameObject;
        levelComplete = false;
        // Unload Previous
        UnloadLastLevel();
        // Set position back to origin
        ResetPositions();
        ShowDropButton();
    }

    private void LoadNextLevel()
    {
        newLevel = Instantiate(levels[level], newLevelLocation.position, Quaternion.identity);
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

    }

    public void UpdateLevelUI() {
        levelUI.text = level.ToString();
    }

    public void Restart() {
        LoadNextLevel();
        UnloadLastLevel();
        ResetPositions();
        dropButtonAnimator.Play("FadeIn");
        dropButtonAnimator.GetComponent<Button>().interactable = true;
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
        dropButton.GetComponent<Button>().interactable = false;
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

    private void ShowDropButton() {
        dropButton.GetComponent<Button>().interactable = true;
        dropButtonAnimator.Play("FadeIn");
    }

    public void Drop() {
        sphere.GetComponent<Rigidbody>().isKinematic = false;
        sphere.GetComponent<Rigidbody>().detectCollisions = true;
        dropButtonAnimator.Play("FadeOut");
        dropButton.GetComponent<Button>().interactable = false;
    }
}
