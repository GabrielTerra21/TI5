using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    private PlayerInput playerINput;
    private void Awake() {
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
    }
}
