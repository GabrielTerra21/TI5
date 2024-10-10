using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialScreen : MonoBehaviour {

    private void OnEnable() {
        Open();
    }

    public void Open() {
        GameManager.Instance.EnterUI();
        GameManager.Instance.playerInput.actions["Confirm"].performed += Close;
    }

    public void Close(InputAction.CallbackContext context) {
        //Toca animação
        Debug.Log("opa");
        GameManager.Instance.playerInput.actions["Confirm"].performed -= Close;
        gameObject.SetActive(false);
        GameManager.Instance.ExitUI();
    }
    
}
