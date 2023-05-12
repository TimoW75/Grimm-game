using UnityEngine;

public class getClueFromGroundObject : MonoBehaviour
{

    [SerializeField] private Item[] clueItem;
    public InventoryManager inventoryManage;
    private bool givenItem = false;
    [SerializeField] private bool canPickUp;
    [SerializeField] private bool destroyAfter;
    private void OnMouseDown()
    {
        if (!givenItem)
        {
            givenItem = true;
            for (int i = 0; i < clueItem.Length; i++)
            {
                InventoryManager.Instance.AddItem(clueItem[i]);
            }
            inventoryManage.ListItems();
            if (destroyAfter)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (canPickUp && !givenItem)
        {
            givenItem = true;
            for (int i = 0; i < clueItem.Length; i++)
            {
                InventoryManager.Instance.AddItem(clueItem[i]);
            }
            inventoryManage.ListItems();
            if (destroyAfter)
            {
                Destroy(gameObject);
            }
        }
    }
}
