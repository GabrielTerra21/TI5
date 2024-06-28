using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Dialogue", order = 2)]
public class Dialogue : ScriptableObject {
   [Header("List of Speeches in a Dialogue event")]
   [Space(10)]
   [Tooltip("Speeches will be read from top to bottom, so first element must be on top and last on the bottom")]
   public List<Speech> dialogue = new List<Speech>();
}
