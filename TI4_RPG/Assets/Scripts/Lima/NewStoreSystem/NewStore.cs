using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewStore : MonoBehaviour {
    [SerializeField] private List<StoreItem> items = new List<StoreItem>();
    [SerializeField] private GameObject storeInterface;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private TMP_Text ecosAmount;

    public void OpenStore() {
        GameManager.Instance.EnterUI();
        storeInterface.SetActive(true);
        ecosAmount.text = GameManager.Instance.ecos.ToString();
        InputManager.Instance.actions.FindActionMap("MyUI").FindAction("Cancel").performed += CloseStore;
        foreach (var data in items) {
            //data.hoverSkill += () => DisplayInfo(data.skill.Description);
            data.purchased += () => UpdateStore();
        }
    }

    public void CloseStore(InputAction.CallbackContext context) => CloseStore();

    private void DisplayInfo(string description) => itemDescription.text = description;

    private void UpdateStore() {
        ecosAmount.text = GameManager.Instance.ecos.ToString();
    }
    
    public void CloseStore() {
        
        storeInterface.SetActive(false);
        InputManager.Instance.actions.FindActionMap("MyUI").FindAction("Cancel").performed -= CloseStore;
        GameManager.Instance.ExitUI();
    }
    
}
