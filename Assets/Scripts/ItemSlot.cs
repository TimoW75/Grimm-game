
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    Transform parentAfterDrag;
    public void OnDrop(PointerEventData eventData)
    {if(transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DragItem dragItem = dropped.GetComponent<DragItem>();
            dragItem.parentAfterDrag = transform;
        }   
    }
}
