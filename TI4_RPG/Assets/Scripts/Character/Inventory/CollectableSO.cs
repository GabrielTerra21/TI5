using UnityEngine;

public abstract class CollectableSO : ScriptableObject {
    [Space(10)]
    [Header ("Colectable properties")]
    public Sprite icon;
    public string Type;
    public bool stack;
}
