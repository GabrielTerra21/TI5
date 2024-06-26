using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Speech", order = 1)]

[System.Serializable]
public class Speech : ScriptableObject {
    [TextArea]public string text;
    public CharacterDataSO owner;
}
