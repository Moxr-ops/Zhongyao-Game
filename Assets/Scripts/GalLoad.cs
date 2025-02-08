using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalLoad : MonoBehaviour
{
    public Animator animator;

    public void DestroyGal()
    {
        animator.SetBool("Out", true);
        Destroy(this);
    }
}
