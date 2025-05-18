using System;
using System.IO;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    [Serializable]
    public class SaveData
    {
        public InventoryData[] InventoryData;
    }

    [SerializeField]
    private InventoryBox[] inventories;
    [SerializeField]
    private Item startingItem;

    private void Start()
    {
        string savePath = Path.Combine(Application.persistentDataPath, "SaveData.json");
        if (File.Exists(savePath))
        {
            SaveData loadedData = JsonUtility.FromJson<SaveData>(
                File.ReadAllText(savePath));
            for (int inventoryIndex = 0; inventoryIndex < loadedData.InventoryData.Length; inventoryIndex++)
            {
                InventoryBox inventoryBox = inventories[inventoryIndex];
                var correspondingData = loadedData.InventoryData[inventoryIndex];
                inventoryBox.CreateBox(correspondingData.Items);
            }
        }
        else
        {
            //creates starting values
            for (int inventoryIndex = 0; inventoryIndex < inventories.Length; inventoryIndex++)
            {
                InventoryBox inventoryBox = inventories[inventoryIndex];
                Item[] itemSlots = new Item[10];
                if (inventoryIndex == 1) // if creating chest inventory
                {
                    // Add items to it.
                    itemSlots[0] = startingItem;
                    itemSlots[6] = startingItem;
                }

                inventoryBox.CreateBox(itemSlots);
            }

        }
    }

    public void SaveItemData()
    {
        SaveData saveData = new();
        saveData.InventoryData = new InventoryData[inventories.Length];
        for (int inventoryIndex = 0; inventoryIndex < inventories.Length; inventoryIndex++)
        { 
            InventoryBox inventoryBox = inventories[inventoryIndex];
            saveData.InventoryData[inventoryIndex] = new();
            saveData.InventoryData[inventoryIndex].Items = inventoryBox.ItemSlots.Select(box =>  box.CurrentItem).ToArray();
        }

        string savePath = Path.Combine(Application.persistentDataPath, "SaveData.json");
        

        File.WriteAllText(savePath, JsonUtility.ToJson(saveData));
    }
}
