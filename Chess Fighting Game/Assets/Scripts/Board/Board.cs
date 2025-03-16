using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board instance;

    private Space[,] board = new Space[8,8];

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        for (int rank = 0; rank < 8; rank++)
        {
            for (int file = 0; file < 8; file++)
            {
                board[rank, file] = transform.GetChild(rank).GetChild(file).GetComponent<Space>();
            }
        }
    }

    public int GetRank(Space space)
    {
        for (int rank = 0; rank < 8; rank++)
        {
            for (int file = 0; file < 8; file++)
            {
                if (board[rank, file].Equals(space))
                    return rank;
            }
        }
        return -1;
    }

    public int GetFile(Space space)
    {
        for (int rank = 0; rank < 8; rank++)
        {
            for (int file = 0; file < 8; file++)
            {
                if (board[rank, file].Equals(space))
                    return file;
            }
        }
        return -1;
    }

    public Space GetSpace(int rank, int file) => board[rank, file];

    public bool TryFindSpace(int rank, int file, out Space space)
    {
        if (ValidIndex(rank, file))
        {
            space = board[rank, file];
            return true;
        }
        space = null;
        return false;
    }

    private bool ValidIndex(int rank, int file)
    {
        return (0 <= rank && rank < 8) && (0 <= file && file < 8);
    }
}
