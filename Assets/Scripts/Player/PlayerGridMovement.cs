using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGridMovement : MonoBehaviour
{
    [SerializeField]
    private float movementDistance = 0.32f, movementSpeed = 1f;
    private Queue<Vector2> movementCommands;
    private bool isMoving = false, isInputtingMove = false;

    private void Start()
    {
        movementCommands = new();
    }

    public void OnMove(InputValue value)
    {
        Vector2 inputValue = value.Get<Vector2>();
        if(inputValue != Vector2.zero)
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
        Debug.Log("Starting Move coroutine");
        isMoving = true;
        while (movementCommands.Count > 0)
        {
            Vector2 currentCommand = movementCommands.Dequeue();
            Vector2 targetPosition = currentCommand + (Vector2)transform.position;
            while(Vector2.Distance(transform.position, targetPosition) >= movementSpeed/100)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.fixedDeltaTime * movementSpeed);
                yield return new WaitForFixedUpdate();
            }
            transform.position = targetPosition;
            if (isInputtingMove && movementCommands.Count == 0)
                movementCommands.Enqueue(currentCommand);
        }
        isMoving = false;

    }
}
