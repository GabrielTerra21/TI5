using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {
    public Transform spawnPoint;
    [SerializeField] private Portal Destination;
    public string roomID;
    private AsyncOperation sceneToLoad, sceneToUnload;


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(Loading(other.GetComponent<Player>()));
        }
    }
    
    /*
     public void LoadRoom(string nextID, string previousID) {
        GameManager.Instance.PauseGame();
        sceneToLoad = SceneManager.LoadSceneAsync(nextID, LoadSceneMode.Additive);
        sceneToUnload = SceneManager.UnloadSceneAsync(previousID);
        StartCoroutine(Loading());
    }
    */

    IEnumerator Loading(Player player) {
        GameManager.Instance.PauseGame();
        Debug.Log("Loading começou"); 
        GameManager.Instance.vinhette.FadeIn();
        yield return new WaitForSeconds(2);
        sceneToLoad = SceneManager.LoadSceneAsync(Destination.roomID, LoadSceneMode.Additive);
        sceneToUnload = SceneManager.UnloadSceneAsync(roomID);
        yield return new WaitUntil(() => sceneToLoad.isDone);
        player.Teleport(Destination.spawnPoint.position);
        yield return new WaitUntil(() => sceneToUnload.isDone);
        yield return new WaitForSeconds(1);
        GameManager.Instance.vinhette.FadeOut();
        yield return new WaitForSeconds(1);
        GameManager.Instance.UnpauseGame();
    }
    
    
}
