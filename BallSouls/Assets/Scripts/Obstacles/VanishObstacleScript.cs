using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishObstacleScript : MonoBehaviour
{
    [SerializeField] Animation animation;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.ToLower() == "player")
        {
            animation.Play();
        }
    }
}
