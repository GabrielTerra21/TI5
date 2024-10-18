using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialScreen : MonoBehaviour {

    [SerializeField] private GameObject tutorialInterface;

    private void Start() {
        if(!GameManager.Instance.tutorial1) GameManager.Instance.enterCombat.AddListener(Open);
    }

    private void OnDisable() {
        GameManager.Instance.enterCombat.RemoveListener(Open);
    }

    public void Open() {
        GameManager.Instance.EnterUI();
        tutorialInterface.SetActive(true);
        GameManager.Instance.playerInput.actions["Confirm"].performed += Close;
        GameManager.Instance.enterCombat.RemoveListener(Open);
        GameManager.Instance.tutorial1 = true;
    }

    public void Close(InputAction.CallbackContext context) {
        //Toca animação
        GameManager.Instance.playerInput.actions["Confirm"].performed -= Close;
        tutorialInterface.SetActive(false);
        GameManager.Instance.ExitUI();
    }
    
}
