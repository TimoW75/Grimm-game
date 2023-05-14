using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();

    [SerializeField] private Transform inventoryObject;
    [SerializeField] private GameObject itemInventory;
    [SerializeField] private TextMeshProUGUI pickupBox;
    [SerializeField] private GameObject inventory;
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
            if(item.PickedUpFromGround)
            {
                inventory.SetActive(true);
                StartCoroutine(pickupNotifier(item.name));
            }
            ListItems();
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
            if (!item.CompareTag("EmptySlot"))
            {
                Destroy(item.gameObject);
            }
        }

        foreach (var item in items) 
        {
            GameObject obj = Instantiate(itemInventory, inventoryObject);
            var itemIcon = obj.transform.Find("Image").GetComponent<Image>();
            
            itemIcon.sprite = item.itemIcon;
            itemIcon.name = item.itemName;
            itemIcon.preserveAspect = true;
            obj.name = item.itemName;

        }
    }
    private IEnumerator pickupNotifier(string itemName)
    {
        pickupBox.text = "Picked up +1 "+ itemName;
        yield return new WaitForSeconds(2f);
        pickupBox.text = "";
    }

}
