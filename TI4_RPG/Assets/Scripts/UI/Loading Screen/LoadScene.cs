using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour {
    public string scene;
    public void LoadNewScene() {
        GameManager.Instance.LoadNewScene(scene);
    }

    public void LoadSceneAsync() {
        SceneManager.LoadScene(scene);
    }
}
