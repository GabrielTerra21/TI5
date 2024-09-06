using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    
    [SerializeField] private GameObject[] roomTiles;
    [SerializeField] private List<string> discovered = new List<string>();
    [SerializeField] private Dictionary<string, GameObject> tiles = new Dictionary<string, GameObject>();

    private void Start() {
        for (int i = 0; i < roomTiles.Length; i++) {
            tiles.Add("Room" + (i + 1), roomTiles[i]);
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
