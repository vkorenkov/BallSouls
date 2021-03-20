using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Homewor11.InputControl
{
    public class GameMenuScript : MonoBehaviour
    {
        /// <summary>
        /// Поле текущего меню
        /// </summary>
        [SerializeField] GameObject currentState;

        /// <summary>
        /// Коллекция объектов меню
        /// </summary>
        [SerializeField] List<GameObject> _gameObjects;

        bool inMenu;

        private void Update()
        {
            // Выход в меню по нажатию Esc
            if (Input.GetButtonDown(GlobalInput.Cancel) && !inMenu)
            {
                inMenu = !inMenu;
                Pause();
                ChangeState("PauseMenuCanvas");
            }
            // вернуться в игру по нажатию Esc
            else if (Input.GetButtonDown(GlobalInput.Cancel) && inMenu)
            {
                inMenu = !inMenu;
                Back();
                ChangeState("GameMenuCanvas");
            }
        }

        /// <summary>
        /// Метод нажатия на UI кнопку меню
        /// </summary>
        /// <param name="button"></param>
        public void ButtonClick(Button button)
        {
            // Получение текста кнопки 
            var buttonText = button.GetComponentInChildren<Text>().text.ToLower();

            // Выбор действия в соответствии с текстом кнопки
            switch (buttonText.ToLower())
            {
                case "пауза":
                    Pause();
                    button.GetComponentInChildren<Text>().text = "Дальше";
                    break;

                case "дальше":
                    Back();
                    button.GetComponentInChildren<Text>().text = "Пауза";
                    break;

                case "меню":
                    Pause();
                    ChangeState("PauseMenuCanvas");
                    break;

                case "назад":
                    Back();
                    ChangeState("GameMenuCanvas");
                    break;

                case "выбор уровня":
                    LoadMenuScene(SceneManager.sceneCountInBuildSettings - 1);
                    break;

                case "главное меню":
                    LoadMenuScene(0);
                    break;
            }
        }

        /// <summary>
        /// Метод паузы игры
        /// </summary>
        private void Pause()
        {
            Time.timeScale = 0;
            inMenu = true;
        }

        /// <summary>
        /// Метод возврата в игру
        /// </summary>
        private void Back()
        {
            Time.timeScale = 1;
            inMenu = false;
        }

        /// <summary>
        /// Смена меню
        /// </summary>
        /// <param name="stateName"></param>
        private void ChangeState(string stateName)
        {
            if (currentState != null)
            {
                currentState.SetActive(false);

                var newState = _gameObjects.Where(x => x.name == stateName).FirstOrDefault();

                newState.SetActive(true);

                currentState = newState;
            }
        }

        /// <summary>
        /// Метод загрузки сцены
        /// </summary>
        /// <param name="sceneIndex"></param>
        private void LoadMenuScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);

            Time.timeScale = 1;
        }

        /// <summary>
        /// Метод выхода из игры
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }
    }
}
