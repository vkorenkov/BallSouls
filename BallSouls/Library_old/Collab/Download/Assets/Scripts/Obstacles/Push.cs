using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Push : MonoBehaviour
{
    /// <summary>
    /// Поле твердого тела объекта
    /// </summary>
    Rigidbody playerRigitBody;

    /// <summary>
    /// Поле силы отталкивания
    /// </summary>
    [SerializeField] float pushForce = 5;

    void Start()
    {
        // Получение твердого тела объекта
        playerRigitBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collider)
    {
        // условие отталкивания объекта
        if (collider.gameObject.tag.ToLower() == "player")
        {
            // Приложение усилий отталкивания объекта
            playerRigitBody.AddForce(-Camera.main.transform.forward * pushForce, ForceMode.Impulse);
        }
    }
}
