using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBox : MonoBehaviour
{
    [SerializeField]
    private RectTransform inventorySlotPrefab;
    [SerializeField]
    private Image ItemDragAndDropPrefab;
    [SerializeField]
    private GridLayoutGroup inventoryLayout;
    [SerializeField]
    private Item carrotItem;

    private void Start()
    {
        CreateBox(new() { carrotItem, null, null });
    }

    public void CreateBox(List<Item> items)
    {
        foreach(var item in items)
        {
            var newSlot = Instantiate(inventorySlotPrefab, inventoryLayout.transform);
            if (item != null)
            {
                var newItemDragAndDrop = Instantiate(ItemDragAndDropPrefab, newSlot.transform);
                newItemDragAndDrop.sprite = item.InventorySprite;
            }
        }
    }
}
