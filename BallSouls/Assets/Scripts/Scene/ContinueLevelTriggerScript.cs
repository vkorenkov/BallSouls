using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueLevelTriggerScript : MonoBehaviour
{
    [SerializeField] GameObject _levelPart;

    public bool _wasPlayed;

    public bool _selfWasPlayed;

    Animation anim;

    Animation selfAnim;

    private void Start()
    {
        anim = _levelPart.GetComponent<Animation>();

        selfAnim = GetComponentInParent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() == "player")
        {           
            _levelPart.SetActive(true);
            if (!_wasPlayed)
            {
                anim.Play();
                _wasPlayed = true;
                selfAnim.Play();
                _selfWasPlayed = true;
            }
        }
    }
}
