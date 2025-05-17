using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 previousMovement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<PlayerGridMovement>().MovementUpdated += OnMovementUpdated;
    }

    private void OnMovementUpdated(Vector2 movementDirection)
    {
        spriteRenderer.flipX = false;
        if(movementDirection.magnitude > 0)
            animator.Play("Walk " + GetDirectionString(movementDirection));
        else
            animator.Play("Idle " + GetDirectionString(previousMovement));

        previousMovement = movementDirection;
    }

    private string GetDirectionString(Vector2 direction)
    {
        if (direction.x > 0)
            return "Right";
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
            return "Right";
        }
        else if (direction.y > 0)
            return "Up";

        return "Down";
    }
}
