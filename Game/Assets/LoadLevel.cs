using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public Animator UIAnimator;
    public string levelToLoad;

    public void StartLoad()
    {
        UIAnimator.Play("FadeOut");
        SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Single);
    }

    public void Load()
    {
        try
        {
            SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Single);
            Debug.Log("Load Scene " + levelToLoad);
        }
        catch
        {
            Debug.Log("Level Load Not Valid");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
