using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChouseLevelScript : MonoBehaviour
{
    /// <summary>
    /// Метод выбора уровня
    /// </summary>
    /// <param name="levelNumber"></param>
    public void LoadChouseLevel(int levelNumber)
    {
        // Загрузка выбранного уровня
        SceneManager.LoadScene(levelNumber);
    }
}
