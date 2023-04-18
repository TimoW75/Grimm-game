using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteItemFromInventory : MonoBehaviour
{
    private static  GameObject item;
    void Start()
    {
        item = GetComponent<GameObject>();
    }

    // Update is called once per frame
    public static void deleteItem()
    {
        Destroy(item);
    }
}
