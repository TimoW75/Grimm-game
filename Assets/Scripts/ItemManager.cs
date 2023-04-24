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
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Pickup();
            inventoryManage.ListItems();
        }
    }
}
