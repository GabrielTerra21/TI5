 using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private string[] nextScenes;
    [SerializeField] private Scene currentScene;
    public bool midGameCutscene = false, endGameCutscene = false;
    private bool ended = false;
    private AsyncOperation operation;

    private void Awake(){
        try{
            GameManager.Instance.inCutscene = true;
        }
        catch{}
    }

    private void Start(){
        currentScene = gameObject.scene;
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)) EndCutscene();
    }

    // Inicia Coroutina de carregamento da proxima cena
    public void EndCutscene() => StartCoroutine(LoadNextScene());

    // Para cada cena listada no arranjo nextScenes, inicia uma operação de carregamento asincrono.
    // Quando a ultima cena listada terminar de ser carregada, a cena atual é descarregada de maneira
    // asincrona.
    IEnumerator LoadNextScene() {
        if(ended) yield break;
        ended = true;
        if(endGameCutscene){
            SceneManager.LoadScene(nextScenes[0]);
            yield break;
        }
        if(!midGameCutscene){
            foreach(var scene in nextScenes){
                operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            }
            yield return new WaitUntil(() => operation.isDone);
            SceneManager.UnloadSceneAsync(currentScene);
        }
        else{
            try{
                GameManager.Instance.inCutscene = false;
                operation = SceneManager.UnloadSceneAsync(currentScene);
            }
            catch{}
        }
    }
}
