using UnityEngine;

public class getClueFromGroundObject : MonoBehaviour
{

    [SerializeField] private Item[] clueItem;
    public InventoryManager inventoryManage;
    private bool givenItem = false;

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
        }
    }
}
