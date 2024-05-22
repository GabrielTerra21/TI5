using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] private GameObject lojaMenu;

    public void AbreLoja()
    {
        lojaMenu.SetActive(!lojaMenu.active);
    }
}
