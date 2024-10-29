using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    public static MainMenuManager Instance;
    [SerializeField] private CanvasGroup buttons;

    public void BlockButtons() {
        buttons.interactable = false;
        buttons.blocksRaycasts = false;
    }
    
    public void UnblockButtons() {
        buttons.interactable = true;
        buttons.blocksRaycasts = true;
    }
    
}
