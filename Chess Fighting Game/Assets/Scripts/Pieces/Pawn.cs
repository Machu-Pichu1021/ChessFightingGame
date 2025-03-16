using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    protected override void FindValidSquares()
    {
        validSpaces.Clear();
        int currentRank = Board.instance.GetRank(currentSpace);
        int currentFile = Board.instance.GetFile(currentSpace);
        if (color == PieceColor.WHITE)
        {
            if (currentRank == 7)
                Destroy(gameObject);
            validSpaces.Add(Board.instance.GetSpace(currentRank + 1, currentFile));
            if (!hasMoved)
                validSpaces.Add(Board.instance.GetSpace(currentRank + 2, currentFile));
            if (Board.instance.TryFindSpace(currentRank + 1, currentFile - 1, out Space diagonal1) && diagonal1.IsOccupied() && !IsFriendly(diagonal1.OccupyingPiece))
                validSpaces.Add(diagonal1);
            if (Board.instance.TryFindSpace(currentRank + 1, currentFile + 1, out Space diagonal2) && diagonal2.IsOccupied() && !IsFriendly(diagonal2.OccupyingPiece))
                validSpaces.Add(diagonal2);
        }
        else if (color == PieceColor.BLACK)
        {
            if (currentRank == 0)
                Destroy(gameObject);
            validSpaces.Add(Board.instance.GetSpace(currentRank - 1, currentFile));
            if (!hasMoved)
                validSpaces.Add(Board.instance.GetSpace(currentRank - 2, currentFile));
            if (Board.instance.TryFindSpace(currentRank - 1, currentFile - 1, out Space diagonal1) && diagonal1.IsOccupied() && !IsFriendly(diagonal1.OccupyingPiece))
                validSpaces.Add(diagonal1);
            if (Board.instance.TryFindSpace(currentRank - 1, currentFile + 1, out Space diagonal2) && diagonal2.IsOccupied() && !IsFriendly(diagonal2.OccupyingPiece))
                validSpaces.Add(diagonal2);
        }
    }
}
