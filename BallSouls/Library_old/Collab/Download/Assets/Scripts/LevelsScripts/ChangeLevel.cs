using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    /// <summary>
    /// Поле текущей сцены
    /// </summary>
    Scene currentScene;
    /// <summary>
    /// Поле объекта игрока
    /// </summary>
    GameObject player;

    void Start()
    {
        // Получение текущей сцены
        currentScene = SceneManager.GetActiveScene();
        // Получение объекта игрока
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Условие при котором загрузится следующий уровень
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
    }
}
