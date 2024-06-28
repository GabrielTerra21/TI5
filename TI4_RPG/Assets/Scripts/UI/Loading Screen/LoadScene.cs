using UnityEngine;


public class LoadScene : MonoBehaviour {
    public string scene;
    public void LoadNewScene() {
        GameManager.Instance.LoadNewScene(scene);
    }
}
