using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager imInstance;
    private Dictionary<int, GameObject> playerInventory = new Dictionary<int, GameObject>();

    private void Awake() {
        if(imInstance == null){
            imInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    public void AddToInventory(GameObject item, int itemKey){
        playerInventory.Add(itemKey, item);
    }

    public void RemoveFromInventory(int itemKey){
        playerInventory.Remove(itemKey);
    }
}
