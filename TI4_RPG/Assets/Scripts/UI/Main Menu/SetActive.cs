using UnityEngine;

public class BotaoAtivarDesativar : MonoBehaviour
{
    public void ToggleActive(){
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
        
}