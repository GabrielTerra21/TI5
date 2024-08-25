using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour {
    [Header("Player info")] 
    public Player player;
    public int money = 300;
    public MyStat ap = new MyStat(25);
    private Exploring exploring;
    private CombatState combatState;

    [Header("Game Info")] 
    public GameState state;
    public enum GameState {COMBAT, EXPLORATION}
    public bool paused;
    public UnityEvent pauseGame, unpauseGame; 
    public string currentScene;
    public Portal previousDoor;
    private List<string> clearedRooms = new List<string>();

    [Header("UI Components")] 
    public ActionBar actionBar;
    public SkillDataSO empty;
    public Vinhette vinhette;
    public Action UpdateUI;
    
    [Header("Managers Components")]
    public PlayerInput playerInput;
    public RoomManager roomManager;
    
    public static GameManager Instance;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
        DontDestroyOnLoad(this);
    }
    
    private void Start() {
        //playerInput = GetComponent<PlayerInput>();
        //playerInput = FindObjectOfType<PlayerInput>();
        //exploring = FindObjectOfType<Exploring>();
        //combatState = FindObjectOfType<CombatState>();
        //if(exploring != null) exploring.OnSubscribe();
        //if(combatState != null) combatState.OnSubscribe();
        currentScene = SceneManager.GetActiveScene().name;
        state = GameState.EXPLORATION;
        actionBar.UpdateBar(ap);
    }

    public void SpendMoney(int price) { money -= price; }

    public void GainMoney(int amount) { money += amount; }

    public void GainAP(int amount) {
        ap.currentValue += amount;
        actionBar.UpdateBar(ap);
    }

    public void SpendAP() {
        ap.currentValue = 0;
        actionBar.UpdateBar(ap);
    }

    public void LoadNewScene(string sceneName) {
        //MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();
        /*foreach (var script in scripts) {
            if(script.gameObject != gameObject) script.enabled = false;
        }*/
        
        //if(exploring != null)exploring.OnCleanup();
        //if(combatState != null)combatState.OnCleanup();

        if (sceneName == "Fase1") money = 150;
        
        pauseGame.RemoveAllListeners(); 
        unpauseGame.RemoveAllListeners();
        UpdateUI = null;
        paused = false;
        currentScene = sceneName;
        playerInput.enabled = false;
        SceneManager.LoadScene("LoadingScreen");
        playerInput.enabled = true;
    }

    // Checa se o ID da sala informado esta presente na lista de salas completas e retorna o resultado.
    public bool CheckClearedRooms(string ID) {
        return clearedRooms.Contains(ID);
    }

    public void AddClearedRoom(string ID) {
        if(clearedRooms.Contains(ID))return;
        else clearedRooms.Add(ID);
    }

    public void DeathLoad() {
        PauseGame();
        StartCoroutine(LoadPreviousScreen());
        ap.currentValue = 0;
        UIUpdate();
    }

    IEnumerator LoadPreviousScreen() {
        vinhette.Cover();
        
        yield return new WaitForSeconds(2);
        
        AsyncOperation sceneToLoad = SceneManager.LoadSceneAsync(previousDoor.roomID, LoadSceneMode.Additive);
        AsyncOperation sceneToUnload = SceneManager.UnloadSceneAsync(currentScene);
        
        yield return new WaitUntil(() => sceneToLoad.isDone);
        
        player.Teleport(previousDoor.spawnPoint.position);
        
        yield return new WaitUntil(() => sceneToUnload.isDone);
        yield return new WaitForSeconds(1);
        
        Instance.vinhette.Uncover();
        yield return new WaitForSeconds(1);
        
        Instance.UnpauseGame();
    }

    public void PauseGame() {
        if (paused) { throw new Exception("Game is already paused"); }
        if (currentScene == "Menu") return;
        pauseGame.Invoke();
        paused = true;
    }

    public void EnterUI() {
        PauseGame();
        playerInput.SwitchCurrentActionMap("MyUI");
    }

    public void ExitUI() {
        UnpauseGame();
        //cross.UpdateSlots();
        playerInput.SwitchCurrentActionMap("Action");
    }

    public void UnpauseGame() {
        if (!paused) { throw new Exception("Is not paused"); }
        unpauseGame.Invoke();
        paused = false;
    }

    public void TogglePause() {
        if (paused) UnpauseGame();
        else PauseGame();
    }

    public void UIUpdate()
    {
        UpdateUI?.Invoke();
    }
}
