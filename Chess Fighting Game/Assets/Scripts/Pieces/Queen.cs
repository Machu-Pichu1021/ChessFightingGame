using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    protected override void FindValidSquares()
    {
        validSpaces.Clear();
        int currentRank = Board.instance.GetRank(currentSpace);
        int currentFile = Board.instance.GetFile(currentSpace);

        int rank = currentRank + 1;
        int file = currentFile + 1;
        while (Board.instance.TryFindSpace(rank, file, out Space spaceNE))
        {
            if (spaceNE.IsOccupied())
            {
                if (!IsFriendly(spaceNE.OccupyingPiece))
                    validSpaces.Add(spaceNE);
                break;
            }
            validSpaces.Add(spaceNE);
            rank++;
            file++;
        }

        rank = currentRank + 1;
        file = currentFile - 1;
        while (Board.instance.TryFindSpace(rank, file, out Space spaceNW))
        {
            if (spaceNW.IsOccupied())
            {
                if (!IsFriendly(spaceNW.OccupyingPiece))
                    validSpaces.Add(spaceNW);
                break;
            }
            validSpaces.Add(spaceNW);
            rank++;
            file--;
        }

        rank = currentRank - 1;
        file = currentFile + 1;
        while (Board.instance.TryFindSpace(rank, file, out Space spaceSE))
        {
            if (spaceSE.IsOccupied())
            {
                if (!IsFriendly(spaceSE.OccupyingPiece))
                    validSpaces.Add(spaceSE);
                break;
            }
            validSpaces.Add(spaceSE);
            rank--;
            file++;
        }

        rank = currentRank - 1;
        file = currentFile - 1;
        while (Board.instance.TryFindSpace(rank, file, out Space spaceSW))
        {
            if (spaceSW.IsOccupied())
            {
                if (!IsFriendly(spaceSW.OccupyingPiece))
                    validSpaces.Add(spaceSW);
                break;
            }
            validSpaces.Add(spaceSW);
            rank--;
            file--;
        }

        rank = currentRank + 1;
        while (Board.instance.TryFindSpace(rank, currentFile, out Space spaceUp))
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

        file = currentFile + 1;
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
