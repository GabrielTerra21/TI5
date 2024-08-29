using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    public static PlayerInput Instance;


    private void Awake() {
        if (Instance == null) {
            Instance = GetComponent<PlayerInput>();
        }
        else { Destroy(gameObject); }
    }
    
    /*private void Awake() {
        if (GameManager.Instance == null)
        {
            try {
                GameManager.Instance.playerInput = GetComponent<PlayerInput>();
            }
            catch (Exception e) {
                Debug.Log($"{e.Message}");
                StartCoroutine(SetPInput());
            }

            playerINput = GetComponent<PlayerInput>();
        }
    }

    IEnumerator SetPInput() {
            yield return new WaitUntil((() => GameManager.Instance));
            GameManager.Instance.playerInput = GetComponent<PlayerInput>();
    }*/
}
