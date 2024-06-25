using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject open, close;

    public void Open(){
        open.SetActive(true);
        close.SetActive(false);
    }

     public void Close(){
        open.SetActive(false);
        close.SetActive(true);
    }
}
