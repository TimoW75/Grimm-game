using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceNPC : MonoBehaviour
{
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private GameObject inventory;
    public InventoryManager inventoryM;
    private bool isClose;

    void Start()
    {
        choicePanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isClose)
        {
            if (choicePanel.activeInHierarchy)
            {
                choicePanel.SetActive(false);
                inventory.SetActive(false);
                DestroyImageChildrenWithTag(choicePanel.transform, "EmptySlot");
            }
            else
            {
                choicePanel.SetActive(true);
                inventory.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isClose = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isClose = false;
        choicePanel.SetActive(false);
        inventory.SetActive(false);
        DestroyImageChildrenWithTag(choicePanel.transform, "EmptySlot");
    }

    void DestroyImageChildrenWithTag(Transform parent, string tag)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);

            if (child.CompareTag(tag))
            {
                Transform grandChild = child.GetChild(0);
                Image image = grandChild.GetComponentInChildren<Image>();
                if (image != null)
                {
                    Destroy(image.gameObject);
                    inventoryM.ListItems();

                }
                break;
            }
        }
    }


}
