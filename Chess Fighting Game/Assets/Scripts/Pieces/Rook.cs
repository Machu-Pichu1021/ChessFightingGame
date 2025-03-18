using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    protected override void FindValidSquares()
    {
        validSpaces.Clear();
        int currentRank = Board.instance.GetRank(currentSpace);
        int currentFile = Board.instance.GetFile(currentSpace);

        int rank = currentRank + 1;
        while(Board.instance.TryFindSpace(rank, currentFile, out Space spaceUp))
        {
            if (spaceUp.IsOccupied())
            {
                if (!IsFriendly(spaceUp.OccupyingPiece))
                    validSpaces.Add(spaceUp);
                break;
            }
            validSpaces.Add(spaceUp);
            rank++;
        }
        rank = currentRank - 1;
        while (Board.instance.TryFindSpace(rank, currentFile, out Space spaceDown))
        {
            if (spaceDown.IsOccupied())
            {
                if (!IsFriendly(spaceDown.OccupyingPiece))
                    validSpaces.Add(spaceDown);
                break;
            }
            validSpaces.Add(spaceDown);
            rank--;
        }

        int file = currentFile + 1;
        while (Board.instance.TryFindSpace(currentRank, file, out Space spaceRight))
        {
            if (spaceRight.IsOccupied())
            {
                if (!IsFriendly(spaceRight.OccupyingPiece))
                    validSpaces.Add(spaceRight);
                break;
            }
            validSpaces.Add(spaceRight);
            file++;
        }
        file = currentFile - 1;
        while (Board.instance.TryFindSpace(currentRank, file, out Space spaceLeft))
        {
            if (spaceLeft.IsOccupied())
            {
                if (!IsFriendly(spaceLeft.OccupyingPiece))
                    validSpaces.Add(spaceLeft);
                break;
            }
            validSpaces.Add(spaceLeft);
            file--;
        }
    }
}
