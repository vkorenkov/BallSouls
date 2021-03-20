using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    Animator menuAnimator;

    bool inMenu;

    private void Start()
    {
        menuAnimator = GetComponent<Animator>();
        menuAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void Update()
    {
        // Выход в меню по нажатию Esc
        if (Input.GetButtonDown("Cancel") && !inMenu)
        {
            inMenu = !inMenu;
            Pause();            
        }
        // вернуться в игру по нажатию Esc
        else if (Input.GetButtonDown("Cancel") && inMenu)
        {
            inMenu = !inMenu;
            Back();
        }
    }

    /// <summary>
    /// Метод паузы игры
    /// </summary>
    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        menuAnimator.SetTrigger("OpenMenu");
        inMenu = true;
    }

    /// <summary>
    /// Метод возврата в игру
    /// </summary>
    public void Back()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        menuAnimator.SetTrigger("CloseMenu");
        inMenu = false;
    }
}
