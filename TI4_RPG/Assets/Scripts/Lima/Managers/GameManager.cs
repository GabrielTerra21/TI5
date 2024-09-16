using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour {
    [Header("Player info")] 
    public Player player;
    public int money = 300;

    [SerializeField] public int keys { get; private set; } = 1;
    public MyStat ap = new MyStat(25);
    private Exploring exploring;
    private CombatState combatState;

    [Header("Game Info")] 
    public GameState state;
    public enum GameState {COMBAT, EXPLORATION, CINEMATIC}
    public bool paused;
    public UnityEvent pauseGame, unpauseGame, enterCombat, enterExploration, enterCinematic; 
    public string currentScene;
    public Portal previousDoor;
    public List<string> clearedRooms = new List<string>();

    [Header("UI Components")] 
    [SerializeField] private TMP_Text keysText;
    public TextHealthBar healthBar;
    public ActionBar actionBar;
    public SkillDataSO empty;
    public Vinhette vinhette;
    //public Vinhette playerStats;
    public Action UpdateUI;
    
    [Header("Managers Components")]
    public PlayerInput playerInput;
    public DialogueBox DialogueManager;
    
    public static GameManager Instance;

    // Codigo de Singleton.
    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
        DontDestroyOnLoad(this);
    }
    
    // Atribui o ID da cena atual ao valor currentScene, determina o stado do GameManager como
    // estado de exploração e faz update da UI.
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
        keysText.text = "Keys : " + keys/10 + keys;
    }

    // Reduz price da quantidade de dinheiro que o jogador tem.
    public void SpendMoney(int price) { money -= price; }

    // Adiciona amount à quantidade de dinheiro do Jogador
    public void GainMoney(int amount) { money += amount; }

    // Adiciona amount à barra de AP do jogador
    public void GainAP(int amount) {
        ap.currentValue += amount;
        actionBar.UpdateBar(ap);
    }

    // Reduz amount pontos da barra de AP do jogador
    public void LoseAP(int amount) {
        ap.currentValue -= amount;
        actionBar.UpdateBar(ap);
    }

    // Zera a barra de AP
    public void SpendAP() {
        ap.currentValue = 0;
        actionBar.UpdateBar(ap);
    }

    public void GainKey() {
        keys++;
        keysText.text = "Keys : " + keys/10 + keys;
    }

    public void LoseKey() {
        keys--;
        keysText.text = "Keys : " + keys/10 + keys;
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

    
    // Adiciona ID à lista de salas vencidas, a menos que a ID informada ja se encontre na lista.
    public void AddClearedRoom(string ID) {
        if(clearedRooms.Contains(ID))return;
        else clearedRooms.Add(ID);
    }

    // Pausa o jogo, chama corotiona para carregar a cena anterior e atualiza as UI's
    // chamado pelo jogador quando ele morre.
    public void DeathLoad() {
        PauseGame();
        StartCoroutine(LoadPreviousScreen());
        ap.currentValue = 0;
        actionBar.UpdateBar(ap);
        healthBar.UpdateValues();
    }

    
    // Cuida das animações da vinheta de morte, teleporta o jogador de volta para a sala anterior
    // e lida com o carregamento e descarregamento de cenas.
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

    // Pausa o jogo e chama o evento de pausa
    public void PauseGame() {
        if (paused) { throw new Exception("Game is already paused"); }
        if (currentScene == "Menu") return;
        pauseGame.Invoke();
        paused = true;
    }

    // Chama a entrada do estado de combate
    public void CallCombatMode() {
        state = GameState.COMBAT;
        StartCoroutine(EventBuffer(enterCombat));
    }

    // Chama a entrada do estado de exploração
    public void CallExploration() {
        state = GameState.EXPLORATION;
        StartCoroutine(EventBuffer(enterExploration));
    }

    public void CallCinematic() {
        state = GameState.CINEMATIC;
        enterCinematic.Invoke();
    }

    // Pausa o jogo e troca o esquema de controles para os controles de UI.
    public void EnterUI() {
        PauseGame();
        playerInput.SwitchCurrentActionMap("MyUI");
    }

    // Despausa o jogo e retorna os controles para o modo de exploração.
    public void ExitUI() {
        UnpauseGame();
        playerInput.SwitchCurrentActionMap("Action");
    }

    // Despausa o jogo e invoca o evento de despausa.
    public void UnpauseGame() {
        if (!paused) { throw new Exception("Is not paused"); }
        unpauseGame.Invoke();
        paused = false;
    }

    // Tenho QUASE certeza que não é utilizado
    public void TogglePause() {
        if (paused) UnpauseGame();
        else PauseGame();
    }

    // Tenho quase certeza que só é utilizado para atualizar a barra de vida
    // Poderia ser refatorado com toda certeza, dito isso, falta tempo...
    public void UIUpdate()
    {
        UpdateUI?.Invoke();
    }

    
    // Aguarda até que o jogo não esteja mais em estado de pausa para invocar o evento
    // necessario para que as animações de interface toquem
    IEnumerator EventBuffer(UnityEvent nEvent) {
        yield return new WaitUntil(() => !paused);
        nEvent.Invoke();
    }
}
