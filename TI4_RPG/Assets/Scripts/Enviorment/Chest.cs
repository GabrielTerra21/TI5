using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator chestAnimator;
    bool isUsed = false;
    public GameObject moneyAnim;
    void Start()
    {
        chestAnimator = GetComponent<Animator>();
    }
    public void OpenChest()
    {
        if (!isUsed)
        {
            if (chestAnimator != null)
            {
                chestAnimator.SetTrigger("OpenChest");
            }
            moneyAnim.SetActive(true);
            GameManager.Instance.GainMoney(150);
            isUsed = true;
        }
    }
}
