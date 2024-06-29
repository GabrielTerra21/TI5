using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationVinesOpen : MonoBehaviour
{
    private Animator mAnimator;
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    public void OpenVines()
    {
        if (mAnimator != null)
        {
            mAnimator.SetTrigger("OpenVine");
        }
    }
}
