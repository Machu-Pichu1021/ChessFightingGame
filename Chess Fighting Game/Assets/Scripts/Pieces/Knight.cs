using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    protected override void FindValidSquares()
    {
        validSpaces.Clear();
        int currentRank = Board.instance.GetRank(currentSpace);
        int currentFile = Board.instance.GetFile(currentSpace);

        if (Board.instance.TryFindSpace(currentRank + 2, currentFile - 1, out Space space1) && (!space1.IsOccupied() || !IsFriendly(space1.OccupyingPiece)))
            validSpaces.Add(space1);
        if (Board.instance.TryFindSpace(currentRank + 2, currentFile + 1, out Space space2) && (!space2.IsOccupied() || !IsFriendly(space2.OccupyingPiece)))
            validSpaces.Add(space2);
        if (Board.instance.TryFindSpace(currentRank + 1, currentFile - 2, out Space space3) && (!space3.IsOccupied() || !IsFriendly(space3.OccupyingPiece)))
            validSpaces.Add(space3);
        if (Board.instance.TryFindSpace(currentRank + 1, currentFile + 2, out Space space4) && (!space4.IsOccupied() || !IsFriendly(space4.OccupyingPiece)))
            validSpaces.Add(space4);
        if (Board.instance.TryFindSpace(currentRank - 1, currentFile - 2, out Space space5) && (!space5.IsOccupied() || !IsFriendly(space5.OccupyingPiece)))
            validSpaces.Add(space5);
        if (Board.instance.TryFindSpace(currentRank - 1, currentFile + 2, out Space space6) && (!space6.IsOccupied() || !IsFriendly(space6.OccupyingPiece)))
            validSpaces.Add(space6);
        if (Board.instance.TryFindSpace(currentRank - 2, currentFile - 1, out Space space7) && (!space7.IsOccupied() || !IsFriendly(space7.OccupyingPiece)))
            validSpaces.Add(space7);
        if (Board.instance.TryFindSpace(currentRank - 2, currentFile + 1, out Space space8) && (!space8.IsOccupied() || !IsFriendly(space8.OccupyingPiece)))
            validSpaces.Add(space8);
    }
}
