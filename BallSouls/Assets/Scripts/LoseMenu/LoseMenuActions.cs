using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenuActions : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StarNewGame()
    {
        SceneManager.LoadScene("L1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
