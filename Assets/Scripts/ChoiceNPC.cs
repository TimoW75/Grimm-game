using UnityEngine;
using UnityEngine.UI;

public class ChoiceNPC : MonoBehaviour
{
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject[] Field;
    [SerializeField] private string[] rightWords;
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
                DestroyImageChildrenWithTag();
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
        checkFilledFields();
        DestroyImageChildrenWithTag();
    }

    void DestroyImageChildrenWithTag()
    {
        for (int i = 0; i < Field.Length; i++)
        {
            foreach (Transform child in Field[i].transform)
            {
                Destroy(child.gameObject);
                
            }
        }
        inventoryM.ListItems();
    }

    void checkFilledFields()
    {
        for (int i = 0; i < Field.Length; i++)
        {
            if (Field[i].gameObject.transform.childCount != 0)
            {
                if (Field[0].gameObject.transform.GetChild(0).name == rightWords[i])
                {
                    print("number " + i + " is right");
                }
                else
                {
                    print("number " + i + " is wrong");
                }
            } else print("not filled in");

        }
    }
}
