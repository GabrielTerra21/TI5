using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    public String Name;
    

    private void OnDrawGizmos(){
        Collider col = GetComponent<Collider>();
        Gizmos.color = new Color(0, 0, 1, .15f);
        Gizmos.DrawCube(col.bounds.center, col.bounds.extents * 2);
    }
}
