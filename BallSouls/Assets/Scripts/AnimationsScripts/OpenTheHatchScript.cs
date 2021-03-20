using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OpenTheHatchScript : MonoBehaviour
{
    /// <summary>
    /// Поле объекта аниматор
    /// </summary>
    Animation _animations;

    /// <summary>
    /// Поле обозначения открыт ли люк
    /// </summary>
    public bool _isOpen;

    // Start is called before the first frame update
    void Start()
    {
        // Получение объекта аниматор
        _animations = GetComponent<Animation>();
    }

    /// <summary>
    /// Метод открытия/закрытия люка
    /// </summary>
    public void OpenClose()
    {
        // Условия при выполнении которых люе закроется или откроется
        if (!_isOpen)
        {
            // Запуск анимации открытия люка
            _animations.Play("HatchAnimation");
            _isOpen = !_isOpen;
        }
        else
        {
            // Запуск анимации закрытия люка
            _animations.Play("CloseHatchAnimation");
            _isOpen = !_isOpen;
        }
    }
}
