using System.Collections.Generic;

public class Inventory{
    //public List<Collectable> inventory = new List<Collectable>(30);
    public Dictionary<CollectableSO, int> inventory = new Dictionary<CollectableSO, int>();

    
    public virtual void Add(CollectableSO got, int amount = 1) {
        if (got.stack) {
            if (inventory.ContainsKey(got))  inventory[got] += amount;
            else inventory.Add(got, amount);
        }
    }

    public virtual bool Remove(CollectableSO item, int amount = 1) {
        if (inventory.ContainsKey(item)) {
            if (inventory[item] <= amount) inventory.Remove(item);
            else inventory[item] -= amount;
            return true;
        }
        return false;
    }
    
    /*public virtual void Add(Collectable got) {
        if (got.data.stack) {
            foreach (var data in inventory) {
                if (data.data.type == got.data.type) {
                    data.Stack(got.amount);
                    return;
                }
            }
        }
        inventory.Add(got);
        inventory.Sort();
    }*/

    /*public virtual void Remove(string type, int removeAmount = -1) {
        foreach (var data in inventory) {
            if (data.data.type == type) {
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
    }*/
}
