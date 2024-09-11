using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour {
    public GameObject pauseScreen;
    public GameObject volumeScreen;
    //public PlayerInput playerInput;
    
    public void OpenPauseMenu(InputAction.CallbackContext context) {
        if (context.performed && GameManager.Instance.paused == false) {
            pauseScreen.SetActive(true);
            volumeScreen.SetActive(false);
            GameManager.Instance.EnterUI();
            InputManager.Instance.actions["Cancel"].performed += ClosePauseMenu;
        }
    }

    public void ClosePauseMenu(InputAction.CallbackContext context) {
        if (context.performed) {
            GameManager.Instance.ExitUI();
            pauseScreen.SetActive(false);
            volumeScreen.SetActive(false);
            InputManager.Instance.actions["Cancel"].performed -= ClosePauseMenu;
        }
    }

    private void OnEnable() { 
        InputManager.Instance.actions["OpenPauseMenu"].performed += OpenPauseMenu;
    }

    public void OnDisable() {
        if(InputManager.Instance)InputManager.Instance.actions["OpenPauseMenu"].performed -= OpenPauseMenu;
        if(InputManager.Instance)InputManager.Instance.actions["Cancel"].performed -= ClosePauseMenu;
    }
}
