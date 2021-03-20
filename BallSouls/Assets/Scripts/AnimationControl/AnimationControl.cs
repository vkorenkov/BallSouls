using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    void ChouseAnimationAction(Animator animator, string triggerParameterName)
    {
        animator.SetTrigger(triggerParameterName);
    }

    void ChouseAnimationAction(Animator animator, string parameterName, int intParameter)
    {
        animator.SetInteger(parameterName, intParameter);
    }

    void ChouseAnimationAction(Animator animator, string parameterName, float floatParameter)
    {
        animator.SetFloat(parameterName, floatParameter);
    }

    void ChouseAnimationAction(Animator animator, string parameterName, bool boolParameter)
    {
        animator.SetBool(parameterName, boolParameter);
    }
}
