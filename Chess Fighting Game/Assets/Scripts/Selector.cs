using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Selector : MonoBehaviour
{
    [SerializeField] private PieceColor color;
    [SerializeField] private Space currentSpace;

    private PlayerInput playerInput;

    private void Start()
    {
        transform.position = currentSpace.transform.position;
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        InputAction moveAction = playerInput.actions["Selector Move"];
        if (moveAction.WasPerformedThisFrame())
        {
            Vector2 input = moveAction.ReadValue<Vector2>();
            print(input);
            int currentRank = Board.instance.GetRank(currentSpace);
            int currentFile = Board.instance.GetFile(currentSpace);

            if (Mathf.Abs(input.y) > Mathf.Abs(input.x))
            {
                if (input.y > 0)
                    Move(currentRank + 1, currentFile);
                else
                    Move(currentRank - 1, currentFile);
            }
            else
            {
                if (input.x > 0)
                    Move(currentRank, currentFile + 1);
                else
                    Move(currentRank, currentFile - 1);
            }
        }
    }

    private void Move(int rank, int file)
    {
        if (Board.instance.TryFindSpace(rank, file, out Space spaceToMove))
        {
            currentSpace = spaceToMove;
            transform.position = currentSpace.transform.position;
        }
        else
        {
            //Play like a little audio thing to indicate that this is an invalid move
            Debug.LogError("Invalid move");
        }
    }
}
