using UnityEngine;

public abstract class State : MonoBehaviour {
    [Header("Info")]
    public bool paused;

    protected virtual void Awake() {
        GameManager.Instance.pauseGame.AddListener((() => paused = true));
        GameManager.Instance.unpauseGame.AddListener((() => paused = false));
    }
    protected virtual void Update() { if(paused) return; }
    protected virtual void FixedUpdate() { if(paused) return; }

    public abstract State OnEnterState();
    public abstract void OnExitState();
}
