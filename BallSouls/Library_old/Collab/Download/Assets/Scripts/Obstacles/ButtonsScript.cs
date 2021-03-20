using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс игровой кнопки
/// </summary>
public class ButtonsScript : MonoBehaviour
{
    /// <summary>
    /// Поле коллекция экземпляров класса выбора анимации других объектов
    /// </summary>
    [SerializeField] List<AnimateChouseScript> animates;

    /// <summary>
    /// Поле анимации кнопки
    /// </summary>
    Animation buttonAnimation;

    /// <summary>
    /// Поле "нажата ли кнопка"
    /// </summary>
    bool isButtonPush;

    private void Start()
    {
        // Получение компонента анимации
        buttonAnimation = GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // условие срабатывания триггера
        if (other.tag.ToLower() == "player" && !isButtonPush)
        {
            // изменение состояния кнопки
            isButtonPush = true;

            // цикл запуска анимаций всех объектов в коллекции
            foreach (var a in animates)
            {
                // Запуск анимации объекта
                a.isAnimationStart = true;
            }

            // запуск анимации кнопки
            buttonAnimation.Play();
        }
    }
}
