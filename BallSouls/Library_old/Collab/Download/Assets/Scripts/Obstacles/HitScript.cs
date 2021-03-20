using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Условие активации урона
        if(collision.gameObject.tag.ToLower() == "player" && !PlayerHeathScript.isHit)
        {
            // Получил ли игрок урон
            PlayerHeathScript.isHit = true;
            // Уменьшение здоровья игрока
            PlayerHeathScript.lives -= 1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Условие активации урона
        if(other.tag.ToLower() == "player")
        {
            // Уменьшение здоровья игрока
            PlayerHeathScript.lives -= 3;
        }
    }
}
