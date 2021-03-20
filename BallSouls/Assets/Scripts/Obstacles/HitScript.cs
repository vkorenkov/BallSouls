using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    PlayerHeathScript PlayerHealth;

    [SerializeField] int damage = 34;

    private void Start()
    {
        PlayerHealth = GameObject.Find("SpherePlayerObj").GetComponent<PlayerHeathScript>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Условие активации урона
        if (collision.gameObject.tag.ToLower() == "player" && !PlayerHealth.isHit)
        {
            GetPlayerDamage(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Условие активации урона
        if (other.tag.ToLower() == "player" && !PlayerHealth.isHit)
        {
            GetPlayerDamage(true);
        }
    }

    void GetPlayerDamage(bool isDeath)
    {
        PlayerHealth.isHit = true;

        if (isDeath) PlayerHealth.health -= PlayerHealth.health;
        else PlayerHealth.health -= damage;

        if (PlayerHealth.health <= 0) PlayerHealth.lives -= 1;
    }
}
