using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour {
    public AsyncOperation scenesToLoad;
    public AsyncOperation scenesToUnload;
    

    public AsyncOperation LoadRoom(string nextID, string previousID) {
        GameManager.Instance.PauseGame();
        scenesToLoad = SceneManager.LoadSceneAsync(nextID, LoadSceneMode.Additive);
        scenesToUnload = SceneManager.UnloadSceneAsync(previousID);
        StartCoroutine(Loading());
        return scenesToLoad;
    }

    IEnumerator Loading() {
        Debug.Log("Loading começou");
        yield return new WaitUntil(() => scenesToLoad.isDone);
        yield return new WaitUntil(() => scenesToUnload.isDone);
        GameManager.Instance.UnpauseGame();
    }
}
