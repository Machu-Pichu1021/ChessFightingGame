using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Selector : MonoBehaviour
{
    [SerializeField] private Space currentSpace;
    private Space selectedSpace;

    private PlayerInput playerInput;

    [SerializeField] private AudioClip invalidSFX;

    private void Start()
    {
        transform.position = currentSpace.transform.position;
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        selectedSpace = null;
    }

    void Update()
    {
        InputAction moveAction = playerInput.actions["Selector Move"];
        InputAction selectAction = playerInput.actions["Selector Select"];
        InputAction deselectAction = playerInput.actions["Selector Deselect"];
        if (moveAction.WasPerformedThisFrame())
        {
            Vector2 input = moveAction.ReadValue<Vector2>();
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
        else if (selectAction.WasPerformedThisFrame())
        {
            currentSpace.OnSelect();
            if (currentSpace.IsOccupied() && selectedSpace == null)
                selectedSpace = currentSpace;
        }
        else if (deselectAction.WasPerformedThisFrame())
        {
            selectedSpace?.OnDeselect();
            selectedSpace = null;
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
            AudioManager.instance.PlaySFX(invalidSFX);
            Debug.LogError("Invalid move");
        }
    }
}
