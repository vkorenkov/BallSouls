using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheDoorScript : MonoBehaviour
{
    /// <summary>
    /// Поле объекта аниматор
    /// </summary>
    [SerializeField] Animator _animator;

    void OnTriggerEnter(Collider other)
    {
        // Условие при котором анимацию активирует только объект player
        if (other.tag.ToLower() == "player")
            _animator.SetBool("open", true);
    }

    void OnTriggerExit(Collider other)
    {
        // Условие при котором анимацию активирует только объект player
        if (other.tag.ToLower() == "player")
            _animator.SetBool("open", false);
    }
}
