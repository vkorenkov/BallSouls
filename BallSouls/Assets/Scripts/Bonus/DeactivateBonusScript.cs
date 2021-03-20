using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateBonusScript : MonoBehaviour
{
    [SerializeField] ParticleSystem _bonusParticle;

    public void PlayParticleBonus()
    {
        _bonusParticle.Play();
    }

    public void DeactivateBonus()
    {
        gameObject.SetActive(false);
    }
}
