using UnityEngine;

public class PressurePlate : MonoBehaviour {
    public enum CLEARSTATE {
        PROGRESS,
        CLEAR,
        WRONG,
        NEUTRAL
    }
    private MaterialPropertyBlock mpb;
    [SerializeField] private CLEARSTATE state;
    [SerializeField] private MaterialPropertyBlock Mpb {
        get {
            if (mpb == null) {
                mpb = new MaterialPropertyBlock();
            }
            return mpb;
        }
    }
    [SerializeField] private Renderer render;
    [SerializeField] private Color inactiveColor, activeColor, rightColor, wrongColor;
    [SerializeField] private int colorID;
    [SerializeField] private ButtonPuzzle manager;
    public int plateID = 0;


    private void Awake() {
        colorID = Shader.PropertyToID("_Color");
        if (!render) render = GetComponent<Renderer>();
        state = CLEARSTATE.NEUTRAL;
    }

    public void SetManager(ButtonPuzzle instance) => manager = instance;
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && state == CLEARSTATE.NEUTRAL) {
            Pressed();
        }
    }

    private void Pressed() {
        manager.Step(plateID);
    }
}
