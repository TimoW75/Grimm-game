using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();

    [SerializeField] private Transform inventoryObject; // Reference to the inventory's transform.
    [SerializeField] private GameObject itemInventory; // Prefab for the item inventory UI.
    [SerializeField] private TextMeshProUGUI pickupBox; // Text object to display pickup notifications.
    [SerializeField] private GameObject inventory; // Reference to the inventory UI.

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!"); // Warns if multiple instances of InventoryManager are found.
            return;
        }
        Instance = this; // Sets the instance to this InventoryManager.
    }

    public void AddItem(Item item)
    {
        if (!item.isDefaultItem) // Checks if the item is not a default item.
        {
            items.Add(item); // Adds the item to the list.
            if (item.PickedUpFromGround) // Checks if the item was picked up from the ground.
            {
                inventory.SetActive(true); // Activates the inventory UI.
                StartCoroutine(pickupNotifier(item.name)); // Starts a coroutine to display a pickup notification.
            }
            ListItems(); // Updates the inventory UI.
        }
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item); // Removes the specified item from the list.
    }

    public void ListItems()
    {
        foreach (Transform item in inventoryObject)
        {
            if (!item.CompareTag("EmptySlot")) // Checks if the item is not an empty slot.
            {
                Destroy(item.gameObject); // Destroys the existing item object in the inventory.
            }
        }

        foreach (var item in items)
        {
            GameObject obj = Instantiate(itemInventory, inventoryObject); // Instantiates a new item object in the inventory.
            var itemIcon = obj.transform.Find("Image").GetComponent<Image>(); // Reference to the item's icon image component.

            itemIcon.sprite = item.itemIcon; // Sets the sprite of the item icon.
            itemIcon.name = item.itemName; // Sets the name of the item icon.
            itemIcon.preserveAspect = true; // Preserves the aspect ratio of the item icon.
            obj.name = item.itemName; // Sets the name of the item object.
        }
    }

    private IEnumerator pickupNotifier(string itemName)
    {
        pickupBox.text = "Picked up +1 " + itemName; // Displays a pickup notification.
        yield return new WaitForSeconds(2f); // Waits for 2 seconds.
        pickupBox.text = ""; // Clears the pickup notification.
    }


}
