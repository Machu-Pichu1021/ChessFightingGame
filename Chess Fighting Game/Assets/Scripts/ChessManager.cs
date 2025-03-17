using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class ChessManager : MonoBehaviour
{
    public static ChessManager instance;

    private PieceColor turnColor = PieceColor.WHITE;
    public PieceColor TurnColor { get => turnColor; }

    [SerializeField] private Selector whiteSelector;
    [SerializeField] private Selector blackSelector;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void EndTurn()
    {
        if (turnColor == PieceColor.WHITE)
        {
            blackSelector.gameObject.SetActive(true);
            whiteSelector.gameObject.SetActive(false);
            turnColor = PieceColor.BLACK;
        }
        else
        {
            whiteSelector.gameObject.SetActive(true);
            blackSelector.gameObject.SetActive(false);
            turnColor = PieceColor.WHITE;
        }
    }
}
