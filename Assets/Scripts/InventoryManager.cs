using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();

    [SerializeField] private Transform inventoryObject;
    [SerializeField] private GameObject itemInventory;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        Instance = this;
    }
    
    public void AddItem(Item item)
    {
        if (!item.isDefaultItem)
        {
            items.Add(item);
        }
    }
    public void removeItem(Item item)
    {
        items.Remove(item);  
    }

    public void ListItems()
    {
        foreach (Transform item in inventoryObject)
        {
            Destroy(item.gameObject);

        }

        foreach (var item in items) 
        {
            GameObject obj = Instantiate(itemInventory, inventoryObject);
            var itemIcon = obj.transform.Find("Image").GetComponent<Image>();
            
            itemIcon.sprite = item.itemIcon;
        }
    }

}
