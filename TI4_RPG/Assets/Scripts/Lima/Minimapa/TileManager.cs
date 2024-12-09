using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    
    [SerializeField] private bool fase2 = false;
    [SerializeField] private int t = 0;
    [SerializeField] private GameObject[] roomTiles;
    [SerializeField] private List<string> discovered = new List<string>();
    [SerializeField] private Dictionary<string, GameObject> tiles = new Dictionary<string, GameObject>();

    private void Start() {
        int i = 0;
        if (fase2 == true){i = 50;}
        Debug.Log("lenth > "+(roomTiles.Length + i));
        for (t = i; t < roomTiles.Length + i; t++) {
            tiles.Add("Room" + (t + 1), roomTiles[t - i]);
            // Debug.Log("Chave > Room"+ (t + 1));
            // Debug.Log("Valor > " + roomTiles[t - i]);
        }

        foreach( var data in discovered)
            DiscoverRoom(data); 
    }

    
    // Checa se o nome da sala informado existe no grupo de chaves do dicionario
    // e se o memso nome não consta na lista de salas descobertas
    // caso ambas sejam verdade, ativa o tile no minimapa e adiciona o mesmo à lista de salas descobertas
    public void DiscoverRoom(string room) {
        if (tiles.ContainsKey(room) && !discovered.Contains(room)) {
            tiles[room].SetActive(true);
            discovered.Add(room);
        }
    }
}
