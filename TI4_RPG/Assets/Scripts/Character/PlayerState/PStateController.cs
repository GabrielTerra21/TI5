using UnityEngine;

public class PStateController : MonoBehaviour
{
    public PlayerState exploring, combat;
    public Player player;
    [SerializeField] PlayerState currentState;

    private void Awake(){
        player = GetComponent<Player>();
        exploring = new ExploringState(player);
        //Put combat state initialization;
    }

    private void Start(){
        CallExporingState();
    }

    public void CallExporingState(){
        exploring.OnEnterState();
        currentState = exploring;
    }
    public void CallCombatState(){
        exploring.OnEnterState();
        currentState = combat;
    }
}
