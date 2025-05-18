using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGridMovement : MonoBehaviour
{
    public event Action<Vector2> MovementUpdated;
    public bool EnableMovement
    {
        get => enableMovement; 
        set
        {
            if (!value)
                movementCommands.Clear();
            enableMovement = value;
        }
    }
    private bool enableMovement;

    [SerializeField]
    private float movementDistance = 0.32f, movementSpeed = 1f;
    private Queue<Vector2> movementCommands;
    private bool isMoving = false, isInputtingMove = false;

    private void Start()
    {
        movementCommands = new();
        EnableMovement = true;
    }

    public void OnMove(InputValue value)
    {
        if (!EnableMovement)
            return;
        Vector2 inputValue = value.Get<Vector2>();
        if (inputValue != Vector2.zero)
        {
            movementCommands.Clear();
            if (inputValue.x != 0)
                movementCommands.Enqueue(new Vector2(movementDistance * Mathf.Sign(inputValue.x), 0));
            else
                movementCommands.Enqueue(new Vector2(0, movementDistance * Mathf.Sign(inputValue.y)));

            //If player wasn't already moving
            if (!isMoving)
                StartCoroutine(MoveTowards()); // start moving
            isInputtingMove = true;
        }
        else
            isInputtingMove = false;

    }

    private IEnumerator MoveTowards()
    {
        isMoving = true;
        while (movementCommands.Count > 0)
        {
            Vector2 currentCommand = movementCommands.Dequeue();
            Vector2 targetPosition = currentCommand + (Vector2)transform.position;
            if (!Physics2D.OverlapCircle(targetPosition, 0.04f))
            {
                MovementUpdated?.Invoke(currentCommand);

                while (Vector2.Distance(transform.position, targetPosition) >= movementSpeed / 1000f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.fixedDeltaTime * movementSpeed);
                    yield return new WaitForFixedUpdate();
                }
                transform.position = targetPosition;
                if (isInputtingMove && movementCommands.Count == 0)
                    movementCommands.Enqueue(currentCommand);
            }
        }
        MovementUpdated?.Invoke(Vector2.zero);

        isMoving = false;

    }
}
