using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private string[] nextScenes;
    [SerializeField] private Scene currentScene;
    private AsyncOperation operation;


    private void Start(){
        currentScene = gameObject.scene;
    }

    private void Update(){
        if(Input.GetKey(KeyCode.Escape)) EndCutscene();
    }

    // Inicia Coroutina de carregamento da proxima cena
    public void EndCutscene() => StartCoroutine(LoadNextScene());

    // Para cada cena listada no arranjo nextScenes, inicia uma operação de carregamento asincrono.
    // Quando a ultima cena listada terminar de ser carregada, a cena atual é descarregada de maneira
    // asincrona.
    IEnumerator LoadNextScene() {
        foreach(var scene in nextScenes){
            operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        }
        yield return new WaitUntil(() => operation.isDone);
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
