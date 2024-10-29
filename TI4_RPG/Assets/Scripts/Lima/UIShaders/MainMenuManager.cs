using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    public static MainMenuManager Instance;
    [SerializeField] private List<Button> buttons;
    private bool menuOpen;

    public void CloseOtherMenus() {
        
    }

    public void OpenNewMenu() {
        foreach (var data in buttons) {
            data.interactable = false;
        }
    }

    public void CloseMenu() {
        foreach (var data in buttons) {
            data.interactable = true;
        }
    }

    private void Start() {
        foreach (var data in buttons) {
            data.onClick.AddListener(OpenNewMenu);
        }
    }
}
