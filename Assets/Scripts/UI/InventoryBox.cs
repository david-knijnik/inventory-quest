using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBox : MonoBehaviour
{
    [SerializeField]
    private InventorySlot inventorySlotPrefab;
    [SerializeField]
    private DragAndDrop ItemDragAndDropPrefab;
    [SerializeField]
    private GridLayoutGroup inventoryLayout;
    public List<InventorySlot> ItemSlots;

    public void CreateBox(Item[] items)
    {
        ItemSlots = new();
        foreach (var item in items)
        {
            var newSlot = Instantiate(inventorySlotPrefab, inventoryLayout.transform);
            ItemSlots.Add(newSlot);
            if (item != null)
            {
                var itemDragAndDrop = Instantiate(ItemDragAndDropPrefab, newSlot.transform);
                itemDragAndDrop.GetComponent<Image>().sprite = item.InventorySprite;
                newSlot.CurrentItem = item;
            }
        }
    }

}
