using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 startingPosition;
    private InventorySlot startingSlot;
    public void OnBeginDrag(PointerEventData eventData)
    {
        startingPosition = transform.position;
        GetComponent<Image>().raycastTarget = false;
        var slot = eventData.hovered.Find(hovered => hovered.GetComponent<InventorySlot>() != null);
        startingSlot = slot.GetComponent<InventorySlot>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = (Vector2)transform.position + eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var slot = eventData.hovered.Find(hovered => hovered.GetComponent<InventorySlot>()!=null);
        if (slot != null)
        {
            slot.GetComponent<InventorySlot>().CurrentItem = startingSlot.CurrentItem;
            startingSlot.CurrentItem = null;
            transform.position = slot.transform.position;
        }
        else
            transform.position = startingPosition;

        GetComponent<Image>().raycastTarget = true;

    }

}
