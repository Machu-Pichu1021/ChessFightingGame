using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Selector : MonoBehaviour
{
    [SerializeField] private PieceColor color;
    [SerializeField] private Space currentSpace;

    private Vector2 input;

    private void Start()
    {
        transform.position = currentSpace.transform.position;
    }

    void Update()
    {
        if (input != Vector2.zero)
        {
            int currentRank = Board.instance.GetRank(currentSpace);
            int currentFile = Board.instance.GetFile(currentSpace);

            if (Mathf.Abs(input.y) > Mathf.Abs(input.x))
            {
                if (input.y > 0)
                    TryMove(currentRank + 1, currentFile);
                else
                    TryMove(currentRank - 1, currentFile);
            }
            else
            {
                if (input.x > 0)
                    TryMove(currentRank, currentFile + 1);
                else
                    TryMove(currentRank, currentFile - 1);
            }
        }
    }

    public void TryMove(int rank, int file)
    {
        if (Board.instance.TryFindSpace(rank, file, out Space spaceToMove))
        {
            currentSpace = spaceToMove;
            transform.position = currentSpace.transform.position;
        }
        else
        {
            //Play like a little audio thing to indicate that this is an invalid move;
            Debug.LogError("Invalid move");
        }
    }

    public void OnMove(InputAction.CallbackContext ctx) => input = ctx.ReadValue<Vector2>();
}
