using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void ChouseLevel()
    {
        animator.SetBool("chouseLevel", true);
    }

    public void BackToMainMenu()
    {
        animator.SetBool("chouseLevel", false);
    }
}
