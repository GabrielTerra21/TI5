using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public List<Collectable> inventory = new List<Collectable>();


    public void Add(Collectable got) {
        if (got.data.stack) {
            foreach (var data in inventory) {
                if (data.data.Type == got.data.Type) {
                    data.Stack(got.amount);
                    return;
                }
            }
        }
        inventory.Add(got);
        inventory.Sort();
    }

    public void Remove(string type, int removeAmount = -1) {
        foreach (var data in inventory) {
            if (data.data.Type == type) {
                if (removeAmount == -1) {
                    inventory.Remove(data);
                    inventory.Sort();
                }
                else {
                    if (data.amount <= removeAmount) {
                        inventory.Remove(data);
                        inventory.Sort();
                    }
                    else data.Remove(removeAmount);
                }
                return;
            }
        }
        Debug.Log("Item a ser removido não foi encontrado");
    }
}
