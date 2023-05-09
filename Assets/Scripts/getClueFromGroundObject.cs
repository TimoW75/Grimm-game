using UnityEngine;

public class getClueFromGroundObject : MonoBehaviour
{

    [SerializeField] private Item clueItem;
    public InventoryManager inventoryManage;
    private bool givenItem = false;

    private void OnMouseDown()
    {
        if (!givenItem)
        {
            givenItem = true;
            InventoryManager.Instance.AddItem(clueItem);
            inventoryManage.ListItems();
        }
    }
}
