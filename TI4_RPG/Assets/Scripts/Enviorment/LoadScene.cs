using UnityEngine;


public class LoadScene : MonoBehaviour {
    public void LoadNewScene(string scene) {
        GameManager.Instance.LoadNewScene(scene);
    }
}
