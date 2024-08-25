using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {
    public Transform spawnPoint;
    public ParticleSystem lights;
    [SerializeField] private Portal Destination;
    public string roomID;
    private AsyncOperation sceneToLoad, sceneToUnload;


    private void Start() {
        GameManager.Instance.enterExploration.AddListener(TurnOn);
        GameManager.Instance.enterCombat.AddListener(TurnOff);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && GameManager.Instance.state != GameManager.GameState.COMBAT) {
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

    public void TurnOff() {
        lights.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    public void TurnOn() {
        lights.Play();
        Debug.Log("");
    }

    IEnumerator Loading(Player player) {
        GameManager.Instance.PauseGame();
        GameManager.Instance.vinhette.FadeIn();
        
        yield return new WaitForSeconds(2);

        GameManager.Instance.previousDoor = this;
        GameManager.Instance.currentScene = Destination.roomID;
        
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
