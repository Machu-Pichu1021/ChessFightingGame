using System.Collections.Generic;
using UnityEngine;

public enum PieceColor
{
    WHITE,
    BLACK
}

public abstract class Piece : MonoBehaviour
{
    protected bool selected;
    protected Space currentSpace;
    protected PieceColor color;
    protected List<Space> validSpaces;

    private void Start()
    {
        FindValidSquares();
    }

    public void OnSelect() {
        if (!selected)
        {
            foreach (Space space in validSpaces)
                space.Highlight();
            selected = true;
        }
        else
            OnDeselect();
    }
    public void OnDeselect()
    {
        foreach (Space space in validSpaces)
        {
            space.Dehighlight();
            validSpaces.Remove(space);
        }
    }
    public void OnMove(Space spaceToMove)
    {
        //currentSpace.IsOccupied = false;
        currentSpace = spaceToMove;
        //currentSpace.IsOccupied = true;
        FindValidSquares();
    }
    protected abstract void FindValidSquares();
}
