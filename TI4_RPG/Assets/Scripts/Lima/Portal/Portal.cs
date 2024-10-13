using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {
    public Transform spawnPoint;
    [SerializeField] private ParticleSystem lights;
    [SerializeField] private WaitingTrigger doorLock;
    [SerializeField] private Portal Destination;
    public string roomID;
    [SerializeField] private AnimationClip fIn, fOut;
    private AsyncOperation sceneToLoad, sceneToUnload;
    [SerializeField] private TileManager tileManager;
    public bool locked = false;

    [SerializeField] private AudioSource audioSource;

    private void Start() {
        GameManager.Instance.enterExploration.AddListener(TurnOn);
        GameManager.Instance.enterCombat.AddListener(TurnOff);
        tileManager = FindObjectOfType<TileManager>();
        if (locked) {
            doorLock.gameObject.SetActive(true);
            doorLock.action.AddListener(Unlock);
            TurnOff();
        }
        else
            doorLock.gameObject.SetActive(false);
    }

    protected void OnTriggerEnter(Collider other) {
        if (locked) return;
        if (other.CompareTag("Player") && GameManager.Instance.state != GameManager.GameState.COMBAT) {
            GameManager.Instance.AddClearedRoom(roomID);
            StartCoroutine(Loading(other.GetComponent<Player>()));
        }
    }

    public void Unlock() {
        if (locked && GameManager.Instance.keys > 0) {
            GameManager.Instance.LoseKey();
            locked = false;
            doorLock.gameObject.SetActive(false);
            TurnOn();
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
        if(lights != null) lights.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    public void TurnOn() {
        if (locked) return;
        if(lights != null) lights.Play();
    }

    IEnumerator Loading(Player player) {
        //GameManager.Instance.state = GameManager.GameState.CINEMATIC;
	audioSource.Play();

        GameManager.Instance.PauseGame();
        GameManager.Instance.vinhette.FadeIn();
        
        yield return new WaitForSeconds(fIn.length);

        GameManager.Instance.previousDoor = this;
        GameManager.Instance.currentScene = Destination.roomID;
        
        sceneToLoad = SceneManager.LoadSceneAsync(Destination.roomID, LoadSceneMode.Additive);
        sceneToUnload = SceneManager.UnloadSceneAsync(roomID);
        
        yield return new WaitUntil(() => sceneToLoad.isDone);
        
        player.Teleport(Destination.spawnPoint.position);
        tileManager.DiscoverRoom(Destination.roomID);
        
        yield return new WaitUntil(() => sceneToUnload.isDone);
        
        GameManager.Instance.vinhette.FadeOut();
        yield return new WaitForSeconds(fOut.length);
        
        GameManager.Instance.UnpauseGame();
        /*
        if (GameManager.Instance.state == GameManager.GameState.CINEMATIC) {
            GameManager.Instance.state = GameManager.GameState.EXPLORATION;
        }
        */
    }
    
    
}
