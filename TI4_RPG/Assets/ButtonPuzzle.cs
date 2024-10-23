using UnityEngine;

public class ButtonPuzzle : MonoBehaviour {
    [SerializeField] private bool complete;
    [SerializeField] private int count;
    [SerializeField] private PressurePlate[] plates;


    private void Awake() {
        complete = false;
        count = 0;
    }

    public PressurePlate.CLEARSTATE Step(int id) {
        if (id == count) {
            count++;
            if (count == plates.Length) {
                Clear();
                return PressurePlate.CLEARSTATE.CLEAR;
            }
            else {
                return PressurePlate.CLEARSTATE.PROGRESS;
            }
        }

        return PressurePlate.CLEARSTATE.WRONG;
    }

    private void Clear() {
        
    }
}
