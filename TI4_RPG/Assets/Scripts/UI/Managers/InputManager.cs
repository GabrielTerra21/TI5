using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private void Awake() { if (GameManager.Instance.playerInput == null) GameManager.Instance.playerInput = GetComponent<PlayerInput>(); }
}
