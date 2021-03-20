using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePushTriggerScript : MonoBehaviour
{
    Animation animation;

    bool isTriggered;

    public bool isPush;

    float startAnimationTime;

    [SerializeField] float firstAnimTimeValue = 0.1f;
    [SerializeField] float secondAnimTimeValue = 4;

    private void Awake()
    {
        animation = GetComponent<Animation>();

        startAnimationTime = Random.Range(firstAnimTimeValue, secondAnimTimeValue);
    }

    private void Update()
    {
        if (isTriggered)
        {
            startAnimationTime -= Time.deltaTime;

            if (startAnimationTime <= 0)
            {
                animation.Play();
                startAnimationTime = Random.Range(firstAnimTimeValue, secondAnimTimeValue);
            }
        }
        else
            animation.Stop();          
    }

    public void AnimationStop()
    {
        isTriggered = false;
    }

    public void IsPush()
    {
        isPush = !isPush;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.ToLower() == "player")
        {
            isTriggered = true;
        }
    }
}
