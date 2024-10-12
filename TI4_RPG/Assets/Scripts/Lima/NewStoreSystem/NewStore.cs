using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewStore : MonoBehaviour {
    [SerializeField] private List<StoreItem> items = new List<StoreItem>();
    [SerializeField] private GameObject storeInterface;

    public void OpenStore() {
        GameManager.Instance.EnterUI();
        storeInterface.SetActive(true);
        InputManager.Instance.actions.FindActionMap("MyUI").FindAction("Cancel").performed += CloseStore;
    }

    public void CloseStore(InputAction.CallbackContext context) => CloseStore(); 
    
    public void CloseStore() {
        storeInterface.SetActive(false);
        InputManager.Instance.actions.FindActionMap("MyUI").FindAction("Cancel").performed -= CloseStore;
        GameManager.Instance.ExitUI();
    }
}
