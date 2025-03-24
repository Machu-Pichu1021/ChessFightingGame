using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string[,] piecePositions = new string[8, 8];

    [SerializeField] private GameObject whitePawnPrefab;
    [SerializeField] private GameObject whiteKnightPrefab;
    [SerializeField] private GameObject whiteBishopPrefab;
    [SerializeField] private GameObject whiteRookPrefab;
    [SerializeField] private GameObject whiteQueenPrefab;
    [SerializeField] private GameObject whiteKingPrefab;

    [SerializeField] private GameObject blackPawnPrefab;
    [SerializeField] private GameObject blackKnightPrefab;
    [SerializeField] private GameObject blackBishopPrefab;
    [SerializeField] private GameObject blackRookPrefab;
    [SerializeField] private GameObject blackQueenPrefab;
    [SerializeField] private GameObject blackKingPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        StartCoroutine(InstantiatePieces());
    }

    private IEnumerator InstantiatePieces()
    {
        yield return new WaitForEndOfFrame();
        for (int file = 0; file < 8; file++)
        {
            Pawn whitePawn = Instantiate(whitePawnPrefab).GetComponent<Pawn>();
            Pawn blackPawn = Instantiate(blackPawnPrefab).GetComponent<Pawn>();
            Board.instance.GetSpace(1, file).Occupy(whitePawn);
            whitePawn.SetCurrentSpace(Board.instance.GetSpace(1, file));
            Board.instance.GetSpace(6, file).Occupy(blackPawn);
            blackPawn.SetCurrentSpace(Board.instance.GetSpace(6, file));
        }

        Knight whiteKnight1 = Instantiate(whiteKnightPrefab).GetComponent<Knight>();
        Board.instance.GetSpace(0, 1).Occupy(whiteKnight1);
        whiteKnight1.SetCurrentSpace(Board.instance.GetSpace(0, 1));
        Knight whiteKnight2 = Instantiate(whiteKnightPrefab).GetComponent<Knight>();
        Board.instance.GetSpace(0, 6).Occupy(whiteKnight2);
        whiteKnight2.SetCurrentSpace(Board.instance.GetSpace(0, 6));
        Knight blackKnight1 = Instantiate(blackKnightPrefab).GetComponent<Knight>();
        Board.instance.GetSpace(7, 1).Occupy(blackKnight1);
        blackKnight1.SetCurrentSpace(Board.instance.GetSpace(7, 1));
        Knight blackKnight2 = Instantiate(blackKnightPrefab).GetComponent<Knight>();
        Board.instance.GetSpace(7, 6).Occupy(blackKnight2);
        blackKnight2.SetCurrentSpace(Board.instance.GetSpace(7, 6));

        Bishop whiteBishop1 = Instantiate(whiteBishopPrefab).GetComponent<Bishop>();
        Board.instance.GetSpace(0, 2).Occupy(whiteBishop1);
        whiteBishop1.SetCurrentSpace(Board.instance.GetSpace(0, 2));
        Bishop whiteBishop2 = Instantiate(whiteBishopPrefab).GetComponent<Bishop>();
        Board.instance.GetSpace(0, 5).Occupy(whiteBishop2);
        whiteBishop2.SetCurrentSpace(Board.instance.GetSpace(0, 5));
        Bishop blackBishop1 = Instantiate(blackBishopPrefab).GetComponent<Bishop>();
        Board.instance.GetSpace(7, 2).Occupy(blackBishop1);
        blackBishop1.SetCurrentSpace(Board.instance.GetSpace(7, 2));
        Bishop blackBishop2 = Instantiate(blackBishopPrefab).GetComponent<Bishop>();
        Board.instance.GetSpace(7, 5).Occupy(blackBishop2);
        blackBishop2.SetCurrentSpace(Board.instance.GetSpace(7, 5));

        Rook whiteRook1 = Instantiate(whiteRookPrefab).GetComponent<Rook>();
        Board.instance.GetSpace(0, 0).Occupy(whiteRook1);
        whiteRook1.SetCurrentSpace(Board.instance.GetSpace(0, 0));
        Rook whiteRook2 = Instantiate(whiteRookPrefab).GetComponent<Rook>();
        Board.instance.GetSpace(0, 7).Occupy(whiteRook2);
        whiteRook2.SetCurrentSpace(Board.instance.GetSpace(0, 7));
        Rook blackRook1 = Instantiate(blackRookPrefab).GetComponent<Rook>();
        Board.instance.GetSpace(7, 0).Occupy(blackRook1);
        blackRook1.SetCurrentSpace(Board.instance.GetSpace(7, 0));
        Rook blackRook2 = Instantiate(blackRookPrefab).GetComponent<Rook>();
        Board.instance.GetSpace(7, 7).Occupy(blackRook2);
        blackRook2.SetCurrentSpace(Board.instance.GetSpace(7, 7));

        Queen whiteQueen = Instantiate(whiteQueenPrefab).GetComponent<Queen>();
        Board.instance.GetSpace(0, 3).Occupy(whiteQueen);
        whiteQueen.SetCurrentSpace(Board.instance.GetSpace(0, 3));
        Queen blackQueen = Instantiate(blackQueenPrefab).GetComponent<Queen>();
        Board.instance.GetSpace(7, 3).Occupy(blackQueen);
        blackQueen.SetCurrentSpace(Board.instance.GetSpace(7, 3));

        King whiteKing = Instantiate(whiteKingPrefab).GetComponent<King>();
        Board.instance.GetSpace(0, 4).Occupy(whiteKing);
        whiteKing.SetCurrentSpace(Board.instance.GetSpace(0, 4));
        King blackKing = Instantiate(blackKingPrefab).GetComponent<King>();
        Board.instance.GetSpace(7, 4).Occupy(blackKing);
        blackKing.SetCurrentSpace(Board.instance.GetSpace(7, 4));
    }

    public void Chess()
    {
        StartCoroutine(DelayLoadPositions());
    }

    private IEnumerator DelayLoadPositions()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        yield return new WaitUntil(() => loading.isDone);
        LoadPiecePositions();
    }

    public void Box()
    {
        SavePiecePositions();
        SceneManager.LoadScene(1);
    }

    public void SavePiecePositions()
    {
        for (int rank = 0; rank < 8; rank++)
        {
            for (int file = 0; file < 8; file++)
            {
                Space space = Board.instance.GetSpace(rank, file);
                if (space.IsOccupied())
                    piecePositions[rank, file] = space.OccupyingPiece.name;
            }
        }
    }

    private void LoadPiecePositions()
    {
        for (int rank = 0; rank < 8; rank++)
        {
            for (int file = 0; file < 8; file++)
            {
                if (piecePositions[rank, file] != null)
                {
                    Piece piece = null;
                    string[] pieceInfo = piecePositions[rank, file].Split(" ");
                    string pieceColor = pieceInfo[0];
                    string pieceType = pieceInfo[1];
                    if (pieceColor == "White")
                    {
                        if (pieceType.StartsWith("Pawn"))
                            piece = Instantiate(whitePawnPrefab).GetComponent<Pawn>();
                        else if (pieceType.StartsWith("Knight"))
                            piece = Instantiate(whiteKnightPrefab).GetComponent<Knight>();
                        else if (pieceType.StartsWith("Bishop"))
                            piece = Instantiate(whiteBishopPrefab).GetComponent<Bishop>();
                        else if (pieceType.StartsWith("Rook"))
                            piece = Instantiate(whiteRookPrefab).GetComponent<Rook>();
                        else if (pieceType.StartsWith("Queen"))
                            piece = Instantiate(whiteQueenPrefab).GetComponent<Queen>();
                        else if (pieceType.StartsWith("King"))
                            piece = Instantiate(whiteKingPrefab).GetComponent<King>();
                    }
                    else if (pieceColor == "Black") 
                    {
                        if (pieceType.StartsWith("Pawn"))
                            piece = Instantiate(blackPawnPrefab).GetComponent<Pawn>();
                        else if (pieceType.StartsWith("Knight"))
                            piece = Instantiate(blackKnightPrefab).GetComponent<Knight>();
                        else if (pieceType.StartsWith("Bishop"))
                            piece = Instantiate(blackBishopPrefab).GetComponent<Bishop>();
                        else if (pieceType.StartsWith("Rook"))
                            piece = Instantiate(blackRookPrefab).GetComponent<Rook>();
                        else if (pieceType.StartsWith("Queen"))
                            piece = Instantiate(blackQueenPrefab).GetComponent<Queen>();
                        else if (pieceType.StartsWith("King"))
                            piece = Instantiate(blackKingPrefab).GetComponent<King>();
                    }

                    Space space = Board.instance.GetSpace(rank, file);
                    piece.SetCurrentSpace(space);
                    space.Occupy(piece);
                }
            }
        }
    }
}
