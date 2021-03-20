using Homewor11.InputControl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public static int currentSceneIndex;
    static bool isLoadedSceneAttached;

    // Start is called before the first frame update
    void Start()
    {
        if (currentSceneIndex == 0)
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (!isLoadedSceneAttached)
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            isLoadedSceneAttached = true;
        }
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        GetCurrentScene();

        Time.timeScale = 1;
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadNextScene()
    {
        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(GlobalInput.WinMenuScene);
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadSceneWhenLose()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void GetCurrentScene()
    {
        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

        var activeSceneName = SceneManager.GetActiveScene().name;

        var sceneName = SceneManager.GetActiveScene().name;

        if (activeSceneName.ToLower() != "mainmenu" && sceneName.ToLower() != "losescene")
        {
            currentSceneIndex = activeSceneIndex;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
