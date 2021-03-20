using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Homewor11.InputControl;

public class PlayerHeathScript : MonoBehaviour
{
    /// <summary>
    /// Поле объекта аниматор
    /// </summary>
    Animator animator;

    /// <summary>
    /// Поле изображения здоровья игрока
    /// </summary>
    [SerializeField] Image HealthImage;

    /// <summary>
    /// Поле "получил ли игрок урон"
    /// </summary>
    public static bool isHit;

    private bool isDeath;

    /// <summary>
    /// Время неуязвимости игрока
    /// </summary>
    float postHitTime = 2;

    float postDeathTime = 3;

    /// <summary>
    /// Количество здоровья игрока
    /// </summary>
    [SerializeField] public static int lives = 3;

    RespawnScript _respawn;

    PlayerInputs _inputs;

    void Start()
    {
        // получение объекта аниматор
        animator = GetComponent<Animator>();

        _respawn = GetComponent<RespawnScript>();

        _inputs = GetComponent<PlayerInputs>();

        animator.SetInteger("Health", lives);
    }

    void Update()
    {
        // вызов метода изменения изображения здоровья игрока
        ChangeHealthSprite();

        // условие получил ли игрок урон
        if (isHit)
        {
            // запуск таймера неуязвимости
            postHitTime -= Time.deltaTime;

            // сброс таймера неуязвимости
            if (postHitTime <= 0)
            {
                postHitTime = 2;
                isHit = false;
            }

            // Запуск анимации неуязвимости
            animator.SetBool("isHitAnim", isHit);
        }

        // Перезагрузка сцены при нулевом количесве здоровья
        if (lives == 0)
        {
            isDeath = true;

            animator.SetBool("Death", isDeath);

            _inputs.isControlActive = false;

            postDeathTime -= Time.deltaTime;

            if (postDeathTime <= 0)
            {
                _respawn.RespawnPosition(_inputs.rb, _inputs.startPosition);

                isDeath = false;

                postDeathTime = 3;

                animator.SetBool("Death", isDeath);

                animator.SetInteger("Health", lives);

                _inputs.isControlActive = true;

                lives = 3;
            }
        }
    }

    /// <summary>
    /// Метод изменения изображения здоровья игрока
    /// </summary>
    void ChangeHealthSprite()
    {
        float fill = 0;

        switch (lives)
        {
            case 3:
                fill = 1;
                break;
            case 2:
                fill = 0.6f;
                break;
            case 1:
                fill = 0.3f;
                break;
            case 0:
                fill = 0;
                break;
        }

        HealthImage.fillAmount = fill;
    }
}
