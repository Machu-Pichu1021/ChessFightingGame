using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceColor
{
    WHITE,
    BLACK
}

public abstract class Piece : MonoBehaviour
{
    [SerializeField] protected Space currentSpace;
    [SerializeField] protected PieceColor color;
    public PieceColor Color { get => color; }
    protected List<Space> validSpaces = new List<Space>();
    protected bool hasMoved;
    private bool isSelected;
    public bool IsSelected { get => isSelected; }

    private void Start()
    {
        currentSpace.Occupy(this);
        StartCoroutine(Setup());
    }

    private IEnumerator Setup()
    {
        yield return new WaitForEndOfFrame();
        FindValidSquares();
    }

    public void OnSelect() {
        if (!isSelected)
        {
            foreach (Space space in validSpaces)
                space.Highlight();
            isSelected = true;
        }
        else
            OnDeselect();
    }

    public void OnDeselect()
    {
        foreach (Space space in validSpaces)
            space.Dehighlight();
        isSelected = false;
    }
    public void Move(Space spaceToMove)
    {
        hasMoved = true;
        currentSpace.Deoccupy();
        currentSpace = spaceToMove;
        currentSpace.Occupy(this);
        OnDeselect();
        Piece[] pieces = FindObjectsByType<Piece>(FindObjectsSortMode.None);
        foreach (Piece piece in pieces)
            piece.FindValidSquares();

        ChessManager.instance.EndTurn();
    }

    public bool SquareIsValid(Space space) => validSpaces.Contains(space);

    protected bool IsFriendly(Piece other) => other.color == color;

    protected abstract void FindValidSquares();
}
