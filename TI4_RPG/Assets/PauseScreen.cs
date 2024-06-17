using UnityEngine;

public class PauseScreen : MonoBehaviour {
    public GameObject pauseScreen;
    private void Start() {
        GameManager.Instance.pauseGame.AddListener(() => pauseScreen.SetActive(true));
        GameManager.Instance.unpauseGame.AddListener(() => pauseScreen.SetActive(false));
    }
}
