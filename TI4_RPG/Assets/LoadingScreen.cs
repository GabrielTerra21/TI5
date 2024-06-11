using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {
    public Image bar;


    private void Start() { StartCoroutine(Load()); }
    
    IEnumerator Load() {
        AsyncOperation loading = SceneManager.LoadSceneAsync(GameManager.Instance.currentScene.buildIndex);
        while (!loading.isDone) {
            bar.fillAmount = Mathf.Lerp(0f, 1f, loading.progress);
            yield return null;
        }
    }
}
