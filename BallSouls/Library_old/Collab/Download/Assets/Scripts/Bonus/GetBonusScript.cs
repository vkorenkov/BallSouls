using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBonusScript : MonoBehaviour
{
    [SerializeField] Animator _animator;

    DeactivateBonusScript deactivate;

    private void Start()
    {
        deactivate = GetComponentInParent<DeactivateBonusScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.ToLower() == "player")
        {
            _animator.SetBool("GetBonus", true);

            deactivate.PlayParticleBonus();
        }
    }
}
