using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Hint", order = 1)]
public abstract class HintSO : ScriptableObject
{
    public string title, hintText;
    public bool blocked;
}
