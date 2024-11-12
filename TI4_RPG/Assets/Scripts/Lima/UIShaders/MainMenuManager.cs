using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    public static MainMenuManager Instance;
    [SerializeField] private CanvasGroup buttons;

    public void BlockButtons() {
        buttons.alpha = 0;
        buttons.blocksRaycasts = false;
    }
    
    public void UnblockButtons() {
        buttons.alpha = 1;
        buttons.blocksRaycasts = true;
    }
    
}
