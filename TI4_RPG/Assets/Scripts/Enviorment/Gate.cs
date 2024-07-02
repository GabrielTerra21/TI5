using UnityEngine;

public abstract class Gate : MonoBehaviour
{
    public GameObject open, close;

    public virtual void Open(){
        open.SetActive(true);
        close.SetActive(false);
    }

     public virtual void Close(){
        open.SetActive(false);
        close.SetActive(true);
    }
}
