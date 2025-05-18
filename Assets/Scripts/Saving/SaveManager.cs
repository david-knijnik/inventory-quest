using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Serializable]
    public class SaveData
    {
        public InventoryData[] InventoryData;
    }

    [SerializeField]
    private InventoryBox[] inventories;

    private void Start()
    {
        string savePath = Path.Combine(Application.persistentDataPath, "SaveData.json");
        if (File.Exists(savePath))
        {
            SaveData loadedData = JsonUtility.FromJson<SaveData>(
                File.ReadAllText(savePath));
            for (int inventoryIndex = 0; inventoryIndex < inventories.Length; inventoryIndex++)
            {
                InventoryBox inventoryBox = inventories[inventoryIndex];
                var correspondingData = loadedData.InventoryData[inventoryIndex];
                inventoryBox.CreateBox(correspondingData.Items);
            }
        }
    }
}
