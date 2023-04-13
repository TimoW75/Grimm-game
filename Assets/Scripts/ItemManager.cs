using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item item;
    void Pickup()
    {
        InventoryManager.Instance.AddItem(item);
        Destroy(gameObject);
    }
    private void OnMouseDown()
    {
        Pickup();
    }
}
