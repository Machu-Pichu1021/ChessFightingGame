using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    private Piece occupyingPiece;

    [SerializeField] private GameObject highlighterPrefab;
    private GameObject highlighter;

    public void OnSelect()
    {
        if (IsOccupied())
        {
            occupyingPiece.OnSelect();
        }
        else
        {

        }
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
}
