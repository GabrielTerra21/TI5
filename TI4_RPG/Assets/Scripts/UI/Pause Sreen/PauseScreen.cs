using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour {
    public GameObject pauseScreen;
    
    public void OpenPauseMenu(InputAction.CallbackContext context) {
        if (context.performed) {
            pauseScreen.SetActive(true);
            GameManager.Instance.EnterUI();
            GameManager.Instance.playerInput.actions["Cancel"].performed += ClosePauseMenu;
        }
    }

    public void ClosePauseMenu(InputAction.CallbackContext context) {
        if (context.performed) {
            GameManager.Instance.playerInput.actions["Cancel"].performed -= ClosePauseMenu;
            GameManager.Instance.ExitUI();
            pauseScreen.SetActive(false);
        }
    }
}
