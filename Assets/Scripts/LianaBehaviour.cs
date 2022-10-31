using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LianaBehaviour : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
        enabled = false;
    }
}
