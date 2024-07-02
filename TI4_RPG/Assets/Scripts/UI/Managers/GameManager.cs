using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour{
    [Header("Player info")] 
    public int money = 300;

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
        SceneManager.sceneLoaded += OnLoadScene;
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
        
        // pauseGame.RemoveAllListeners();
        // unpauseGame.RemoveAllListeners();
        // UpdateUI = null;
        paused = false;
        currentScene = sceneName;
        //playerInput.enabled = false;
        SceneManager.LoadScene("LoadingScreen");
    }

    public void OnLoadScene(Scene scene, LoadSceneMode mode) {
        playerInput = FindObjectOfType<PlayerInput>();
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
