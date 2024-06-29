using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Store : MonoBehaviour
{
    [SerializeField] private GameObject storeMenu;
    [SerializeField] private TMP_Text moneyText;
    public GameObject buttonsTutorial;
    
    public void OpenStore() {
        storeMenu.SetActive(true);
        GameManager.Instance.EnterUI();
        GameManager.Instance.playerInput.actions["Cancel"].performed += CloseStore;
        GameManager.Instance.playerInput.actions["InteractButton"].performed += CloseStore;
        UpdateStore();
    }

    public void CloseStore(InputAction.CallbackContext context) {
        GameManager.Instance.playerInput.actions["Cancel"].performed -= CloseStore;
        GameManager.Instance.playerInput.actions["InteractButton"].performed -= CloseStore;
        GameManager.Instance.ExitUI();
        storeMenu.SetActive(false);
        buttonsTutorial.SetActive(true);
    }

    public void UpdateStore() {
        moneyText.text = GameManager.Instance.money.ToString();
    }
}
