using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Homewor11.InputControl;
using System;
using System.Linq;

public class PlayerHeathScript : MonoBehaviour
{
    public event Action LivesEvent;

    /// <summary>
    /// Поле объекта аниматор
    /// </summary>
    Animator animator;

    /// <summary>
    /// Поле "получил ли игрок урон"
    /// </summary>
    public bool isHit;

    private bool isDeath;

    /// <summary>
    /// Время неуязвимости игрока
    /// </summary>
    public float postHitTime = 2;

    public float postDeathTime = 2;

    [SerializeField] public int lives = 1;

    /// <summary>
    /// Количество здоровья игрока
    /// </summary>
    [SerializeField] public int health = 3;

    RespawnScript _respawn;

    PlayerInputs _inputs;

    void Start()
    {
        // получение объекта аниматор
        animator = GetComponent<Animator>();

        _respawn = GetComponent<RespawnScript>();

        _inputs = GetComponent<PlayerInputs>();

        animator.SetInteger("Health", health);
    }

    void Update()
    {
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

        if (health <= 0)
        {
            isDeath = true;

            animator.SetBool("Death", isDeath);

            _inputs.isControlActive = false;

            postDeathTime -= Time.deltaTime;

            if (postDeathTime <= 0)
            {
                if (lives <= 0)
                {
                    LivesEvent?.Invoke();
                }
                else
                {
                    _respawn.RespawnPosition(_inputs.startPosition);

                    isDeath = false;

                    postDeathTime = 2;

                    animator.SetBool("Death", isDeath);

                    animator.SetInteger("Health", health);

                    _inputs.isControlActive = true;

                    health = 100;
                }
            }
        }
    }
}
