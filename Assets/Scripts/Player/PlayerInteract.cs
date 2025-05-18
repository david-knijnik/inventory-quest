using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private Vector2 facingDirection;

    private void Start()
    {
        GetComponent<PlayerGridMovement>().MovementUpdated += SetFacingDirection;
    }

    private void SetFacingDirection(Vector2 newFacingDirection)
    {
        if(newFacingDirection != Vector2.zero)
            facingDirection = newFacingDirection;
    }

    void OnInteract(InputValue inputValue)
    {
        Debug.Log("Interacted");
        var hitCollider = Physics2D.OverlapCircle((Vector2)transform.position + facingDirection, 0.04f);

        if(hitCollider!= null)
        {
            var interactable = hitCollider.GetComponent<IPlayerInteractable>();
            if (interactable != null)
                interactable.Interact();
        }
    }
}
