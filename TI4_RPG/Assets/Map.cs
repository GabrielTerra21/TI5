using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField] private RawImage img;


    private void Awake(){
        img = GetComponent<RawImage>();
        img.enabled = false;
    }

    private void OnEnable(){
        InputManager.Instance.actions["Map"].performed += OpenMap;
        InputManager.Instance.actions["Map"].canceled += CloseMap;
    }

    private void OnDisable(){
        InputManager.Instance.actions["Map"].performed -= OpenMap;
        InputManager.Instance.actions["Map"].canceled -= CloseMap;
    }

    public void OpenMap(InputAction.CallbackContext context) => img.enabled = true;
    public void CloseMap(InputAction.CallbackContext context) => img.enabled = false;
}
