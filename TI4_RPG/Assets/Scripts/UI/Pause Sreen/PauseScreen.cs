using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour {
    public GameObject pauseScreen;
    public PlayerInput playerInput;
    
    public void OpenPauseMenu(InputAction.CallbackContext context) {
        if (context.performed) {
            pauseScreen.SetActive(true);
            GameManager.Instance.EnterUI();
            playerInput.actions["Cancel"].performed += ClosePauseMenu;
        }
    }

    public void ClosePauseMenu(InputAction.CallbackContext context) {
        if (context.performed) {
            if(GameManager.Instance == null) Debug.Log("é");
            GameManager.Instance.ExitUI();
            pauseScreen.SetActive(false);
            playerInput.actions["Cancel"].performed -= ClosePauseMenu;
        }
    }

    private void OnEnable() { 
        playerInput.actions["OpenPauseMenu"].performed += OpenPauseMenu;
    }

    public void OnDisable() {
        playerInput.actions["OpenPauseMenu"].performed -= OpenPauseMenu;
        playerInput.actions["Cancel"].performed -= ClosePauseMenu;
    }
}
