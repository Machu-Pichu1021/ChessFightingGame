using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    protected override void FindValidSquares()
    {
        validSpaces.Clear();
        int currentRank = Board.instance.GetRank(currentSpace);
        int currentFile = Board.instance.GetFile(currentSpace);

        if (Board.instance.TryFindSpace(currentRank + 1, currentFile - 1, out Space spaceNW) && (!spaceNW.IsOccupied() || !IsFriendly(spaceNW.OccupyingPiece)))
            validSpaces.Add(spaceNW);
        if (Board.instance.TryFindSpace(currentRank + 1, currentFile, out Space spaceN) && (!spaceN.IsOccupied() || !IsFriendly(spaceN.OccupyingPiece)))
            validSpaces.Add(spaceN);
        if (Board.instance.TryFindSpace(currentRank + 1, currentFile + 1, out Space spaceNE) && (!spaceNE.IsOccupied() || !IsFriendly(spaceNE.OccupyingPiece)))
            validSpaces.Add(spaceNE);
        if (Board.instance.TryFindSpace(currentRank, currentFile - 1, out Space spaceW) && (!spaceW.IsOccupied() || !IsFriendly(spaceW.OccupyingPiece)))
            validSpaces.Add(spaceW);
        if (Board.instance.TryFindSpace(currentRank, currentFile + 1, out Space spaceE) && (!spaceE.IsOccupied() || !IsFriendly(spaceE.OccupyingPiece)))
            validSpaces.Add(spaceE);
        if (Board.instance.TryFindSpace(currentRank - 1, currentFile + 1, out Space spaceSW) && (!spaceSW.IsOccupied() || !IsFriendly(spaceSW.OccupyingPiece)))
            validSpaces.Add(spaceSW);
        if (Board.instance.TryFindSpace(currentRank - 1, currentFile, out Space spaceS) && (!spaceS.IsOccupied() || !IsFriendly(spaceS.OccupyingPiece)))
            validSpaces.Add(spaceS);
        if (Board.instance.TryFindSpace(currentRank - 1, currentFile - 1, out Space spaceSE) && (!spaceSE.IsOccupied() || !IsFriendly(spaceSE.OccupyingPiece)))
            validSpaces.Add(spaceSE);

        if (!hasMoved)
        {
            Space longCastle = Board.instance.GetSpace(currentRank, 0);
            if (longCastle.IsOccupied() && longCastle.OccupyingPiece is Rook rook1 && !rook1.HasMoved && !Board.instance.GetSpace(currentRank, 1).IsOccupied() && !Board.instance.GetSpace(currentRank, 2).IsOccupied() && !Board.instance.GetSpace(currentRank, 3).IsOccupied())
                validSpaces.Add(Board.instance.GetSpace(currentRank, 2));

            Space shortCastle = Board.instance.GetSpace(currentRank, 7);
            if (shortCastle.IsOccupied() && shortCastle.OccupyingPiece is Rook rook2 && !rook2.HasMoved && !Board.instance.GetSpace(currentRank, 6).IsOccupied() && !Board.instance.GetSpace(currentRank, 5).IsOccupied())
                validSpaces.Add(Board.instance.GetSpace(currentRank, 6));
        }
    }
}
