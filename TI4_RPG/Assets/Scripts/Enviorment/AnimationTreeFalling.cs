using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTreeFalling : MonoBehaviour
{
    private Animator treeAnimator;
    public GameObject colliderBoss;
    void Start()
    {
        treeAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colidiu");
        if (other.CompareTag("FireBall"))
        {
            Debug.Log("Com a bola de fogo");
            FallTree();
            colliderBoss.SetActive(false);
        }
    }

    public void FallTree()
    {
        if (treeAnimator != null)
        {
            treeAnimator.SetTrigger("FallTriger");
        }
    }
}
