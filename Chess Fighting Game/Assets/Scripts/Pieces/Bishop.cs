using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
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
    }
}
