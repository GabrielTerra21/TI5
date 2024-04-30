using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : MonoBehaviour {
    private GameObject menuScreen;
    
    public void OnOpenMenu() {
        menuScreen.SetActive(true);
    }

    public void OnCloseMenu() {
        menuScreen.SetActive(false);
    }
}
