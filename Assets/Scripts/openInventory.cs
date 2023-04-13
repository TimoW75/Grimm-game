using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    public InventoryManager inventoryM;

    void Start()
    {
        inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.E)) 
        {
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
            }
            else
            {
                inventory.SetActive(true);
            }
            inventoryM.ListItems();
        }
    }
}
