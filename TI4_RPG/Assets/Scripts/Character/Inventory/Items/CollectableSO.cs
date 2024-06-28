using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "Data", menuName ="ScriptableObjects/Collectable", order = 1)]
public class CollectableSO : ScriptableObject {
    [Space(10)]
    [Header ("Colectable properties")]
    public Sprite icon;
    public string type;
    public bool stack;
    public int stackAmount;
}
