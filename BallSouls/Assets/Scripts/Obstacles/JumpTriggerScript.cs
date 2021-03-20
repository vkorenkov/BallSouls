using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Усдлвие отталкивания объекта
        if(other.tag.ToLower() == "player")
        {
            // Получение компонента твердого тела
            var rb = other.GetComponent<Rigidbody>();
            
            rb.AddForce(Vector3.up * 13, ForceMode.Impulse);        // Выталкивание объекта
            rb.AddForce(Vector3.forward * 20, ForceMode.Impulse);   //
        }
    }
}
