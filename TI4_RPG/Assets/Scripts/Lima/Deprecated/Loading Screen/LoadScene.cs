using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadScene : MonoBehaviour {
    [SerializeField] private List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    [SerializeField] private Image loadingBar;
    [SerializeField] private GameObject buttons;


    public void LoadSceneAsync() => StartCoroutine(LoadStartScene());

    public void LoadFirstScene() => StartCoroutine(LoadFirstCutscene());
    

    IEnumerator LoadFirstCutscene(){
        buttons.SetActive(false);
        loadingBar.gameObject.SetActive(true);

        scenesToLoad.Add(SceneManager.LoadSceneAsync("FirstScene"));

        float progress = 0;
        while(!scenesToLoad[0].isDone){
            progress += scenesToLoad[0].progress;
            loadingBar.fillAmount = progress / scenesToLoad.Count;
            yield return null;
        }
    }

    IEnumerator LoadStartScene() {
        buttons.SetActive(false);
        loadingBar.gameObject.SetActive(true);
        
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Room1"));
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Doors", LoadSceneMode.Additive));
        
        float progress = 0;
        for (int i = 0; i < scenesToLoad.Count; i++) {
            while (!scenesToLoad[1].isDone) {
                progress += scenesToLoad[i].progress;
                loadingBar.fillAmount = progress / scenesToLoad.Count;
                yield return null;
            }
        }
    }
}
