using UnityEngine;

public abstract class State : MonoBehaviour {
    [Header("Info")]
    public bool paused;

    protected virtual void Start() {
        GameManager.Instance.pauseGame.AddListener((() => paused = true));
        GameManager.Instance.unpauseGame.AddListener((() => paused = false));
    }

    public abstract State OnEnterState();
    public abstract void OnExitState();
}
