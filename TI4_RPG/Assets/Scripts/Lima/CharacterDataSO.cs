using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="ScriptableObjects/CharacterData", order = 1)]
public class CharacterDataSO : ScriptableObject {
    public Sprite profile;
    public string charName;
    public float moveSpeed;
    public int maxHp;
    public int armor;
    public int power;
}
