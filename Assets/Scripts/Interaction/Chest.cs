using UnityEngine;

public class Chest : MonoBehaviour, IPlayerInteractable
{

    public void Interact()
    {
        InventoryUI.Instance.OpenChest();
    }
}
