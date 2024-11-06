using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzleOpen : MonoBehaviour
{
    [SerializeField] private GameObject puzzleInterface;
    [SerializeField] private GameObject padlock;

    public void OpenStore() {
        GameManager.Instance.EnterUI();
        puzzleInterface.SetActive(true);
        padlock.SetActive(true);
        InputManager.Instance.actions.FindActionMap("MyUI").FindAction("Cancel").performed += CloseStore;
    }

    public void CloseStore(InputAction.CallbackContext context) => CloseStore();
    
    public void CloseStore() {
        
        puzzleInterface.SetActive(false);
        padlock.SetActive(false);
        InputManager.Instance.actions.FindActionMap("MyUI").FindAction("Cancel").performed -= CloseStore;
        GameManager.Instance.ExitUI();
    }
}
