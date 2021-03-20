using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateParticlesScript : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    private void Start()
    {
        particle.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        particle.Play();
    }
}
