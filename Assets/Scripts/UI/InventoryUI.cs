using System;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform playerInventory, openedInventory;
    [SerializeField]
    private PlayerGridMovement playerGridMovement;

    //The one instance of this Singleton.
    public static InventoryUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            DestroyImmediate(this);
    }

    public void OpenChest()
    {
        playerInventory.gameObject.SetActive(true);
        openedInventory.gameObject.SetActive(true);
    }

    public void CloseChest()
    {
        playerInventory.gameObject.SetActive(false);
        openedInventory.gameObject.SetActive(false);
        playerGridMovement.EnableMovement = true;
    }
}
