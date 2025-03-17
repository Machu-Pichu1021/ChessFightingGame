using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    private Piece occupyingPiece;
    public Piece OccupyingPiece { get => occupyingPiece; }

    [SerializeField] private GameObject highlighterPrefab;
    private GameObject highlighter;

    [SerializeField] private AudioClip invalidSFX;

    public void OnSelect()
    {
        Piece[] pieces = FindObjectsByType<Piece>(FindObjectsSortMode.None);
        bool isSelectingPiece = !Array.TrueForAll(pieces, piece => !piece.IsSelected);
        if (isSelectingPiece)
        {
            Piece selectedPiece = Array.Find(pieces, piece => piece.IsSelected);
            if (IsOccupied() && occupyingPiece.Equals(selectedPiece))
                occupyingPiece.OnDeselect();
            else if (selectedPiece.SquareIsValid(this))
                selectedPiece.Move(this);
            else
            {
                AudioManager.instance.PlaySFX(invalidSFX);
                Debug.LogError("Can't move this piece here!");
            }
        }
        else
        {
            if (IsOccupied() && occupyingPiece.Color == ChessManager.instance.TurnColor)
                occupyingPiece.OnSelect();
            else
            {
                AudioManager.instance.PlaySFX(invalidSFX);
                Debug.LogError("No piece on this square!");
            }
        }
    }

    public void OnDeselect()
    {
        if (IsOccupied() && occupyingPiece.Color == ChessManager.instance.TurnColor)
            occupyingPiece.OnDeselect();
    }

    public void Highlight()
    {
        highlighter = Instantiate(highlighterPrefab, transform.position, Quaternion.identity);
    }

    public void Dehighlight()
    {
        Destroy(highlighter);
    }

    public bool IsOccupied()
    {
        return occupyingPiece != null;
    }

    public void Deoccupy()
    {
        occupyingPiece = null;
    }

    public void Occupy(Piece piece)
    {
        occupyingPiece = piece;
        piece.transform.position = transform.position;
    }
}
