using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour{
    [Header("Player info")] 
    public int money = 300;
    private Exploring exploring;
    private CombatState combatState;

    [Header("Game Info")] 
    public GameState state;
    public enum GameState {COMBAT, EXPLORATION}
    public bool paused;
    public UnityEvent pauseGame, unpauseGame; 
    public string currentScene;
    
    [Header("UI Components")]
    public SkillDataSO empty;
    public SkillDisplayCross cross;
    public Action UpdateUI;
    
    //[Header("Managers Components")]
    public PlayerInput playerInput;
    
    public static GameManager Instance;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
        DontDestroyOnLoad(this);
    }
    
    private void Start() {
        //playerInput = GetComponent<PlayerInput>();
        SceneManager.sceneLoaded += OnLoadedScene;
        playerInput = FindObjectOfType<PlayerInput>();
        exploring = FindObjectOfType<Exploring>();
        combatState = FindObjectOfType<CombatState>();
        if(exploring != null) exploring.OnSubscribe();
        if(combatState != null) combatState.OnSubscribe();
        currentScene = SceneManager.GetActiveScene().name;
        state = GameState.EXPLORATION;
    }

    public void SpendMoney(int price) {
        money -= price;
    }

    public void GainMoney(int amount) {
        money += amount;
    }

    public void LoadNewScene(string sceneName) {
        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();
        foreach (var script in scripts) {
            if(script.gameObject != gameObject) script.enabled = false;
        }
        
        if(exploring != null)exploring.OnCleanup();
        if(combatState != null)combatState.OnCleanup();

        if (sceneName == "Fase1") money = 150;
        
        pauseGame.RemoveAllListeners(); 
        unpauseGame.RemoveAllListeners();
        UpdateUI = null;
        paused = false;
        currentScene = sceneName;
        SceneManager.LoadScene("LoadingScreen");
    }

    public void OnLoadedScene(Scene scene, LoadSceneMode mode) {
        Debug.Log("oioioioioioi");
        playerInput = FindObjectOfType<PlayerInput>();
        exploring = FindObjectOfType<Exploring>();
        combatState = FindObjectOfType<CombatState>();
        if(exploring != null) exploring.OnSubscribe();
        if(combatState != null) combatState.OnSubscribe();
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
        cross.UpdateSlots();
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
