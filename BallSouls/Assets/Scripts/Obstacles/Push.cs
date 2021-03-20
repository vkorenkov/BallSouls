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
    [SerializeField] float pushForce = 10;

    ActivatePushTriggerScript activatePush;

    void Start()
    {
        // Получение твердого тела объекта
        playerRigitBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

        activatePush = GetComponentInParent<ActivatePushTriggerScript>();
    }

    void OnCollisionEnter(Collision collider)
    {
        // условие отталкивания объекта
        if (collider.gameObject.tag.ToLower() == "player" && activatePush.isPush)
        {
            // Приложение усилий отталкивания объекта
            playerRigitBody.AddForce(-transform.right * pushForce, ForceMode.Impulse);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    // условие отталкивания объекта
    //    if (other.gameObject.tag.ToLower() == "player" && activatePush.isPush)
    //    {
    //        // Приложение усилий отталкивания объекта
    //        playerRigitBody.AddForce(-transform.right * pushForce, ForceMode.Impulse);
    //    }
    //}
}
