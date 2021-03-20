using Homewor11.InputControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    PlayerHeathScript playerHeath;

    void Start()
    {
        playerHeath = GetComponent<PlayerHeathScript>();
        playerHeath.LivesEvent += PlayerHeath_LivesEvent;
    }

    private void PlayerHeath_LivesEvent()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(GlobalInput.LoseSceneName);
        //SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }
}
