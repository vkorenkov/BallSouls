using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateChouseScript : MonoBehaviour
{
    /// <summary>
    /// Поле аниматора
    /// </summary>
    Animator animator;
    /// <summary>
    /// поле имени запускаемой анимации
    /// </summary>
    [SerializeField] string animatrionPropertyName;
    /// <summary>
    /// Поле необходимости нажатия кнопки для запуска анимации
    /// </summary>
    [SerializeField] bool needPushButton;
    /// <summary>
    /// Поле проверки запущенна ли анимация
    /// </summary>
    public bool isAnimationStart;

    // Start is called before the first frame update
    void Start()
    {
        // Получение компонента аниматор
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Метод выбора следующей анимации
    /// </summary>
    public void NextAnimation()
    {
        // Получение количества анимаций
        var animationsCount = animator.runtimeAnimatorController.animationClips.Length;
        // Выбор случайного числа в зависимости от количества анимаций
        int rNum = Random.Range(0, animationsCount);
        
        // Условие необходимости нажатия на кнопку для запуска анимации
        if (needPushButton)
            animator.SetBool("isPlay", isAnimationStart);

        // Случайный выбор анимации
        animator.SetInteger(animatrionPropertyName, rNum);
    }
}
