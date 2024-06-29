using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTreeFalling : MonoBehaviour
{
    private Animator treeAnimator;
    void Start()
    {
        treeAnimator = GetComponent<Animator>();
    }

    public void FallTree()
    {
        if (treeAnimator != null)
        {
            treeAnimator.SetTrigger("FallTriger");
        }
    }
}
