using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item item;
    public InventoryManager inventoryManage;
    void Pickup()
    {
        InventoryManager.Instance.AddItem(item);
        inventoryManage.ListItems();
        Destroy(gameObject);
    }
    private void OnMouseDown()
    {
        Pickup();
    }
}
