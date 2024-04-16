using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour {

    public void LoadNewScene(string scene) {
        SceneManager.LoadScene(scene);
    }
}
