using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public void OpenChest()
    {
        GameManager.Insatance.GanhaDinheiro(100);
        Destroy(gameObject);
    }
}
