using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GridDisplay : MonoBehaviour
{
    [Tooltip("row of this grid")]
    public int row;
    [Tooltip("column of this grid")]
    public int col;

    public Sprite player1Image;
    public Sprite player2Image;

    Image gridUI;

    private void Start()
    {
        gridUI = GetComponent<Image>();
    }

    private void Update()
    {
        var gridState = TicTacToeController.instance.GetGrid(row, col);

        if (gridState == GridState.NONE)
        {
            gridUI.sprite = null;
        }
        else if (gridState == GridState.PLAYER1)
        {
            gridUI.sprite = player1Image;
        }
        else
        {
            gridUI.sprite = player2Image;
        }
    }

    public void MoveGrid()
    {
        var player = TicTacToeController.instance.playerChoose;

        var opponent = (player == GridState.PLAYER1) ? GridState.PLAYER2 : GridState.PLAYER1;

        TicTacToeController.instance.Move(row, col, player);

        if (!TicTacToeController.instance.isGameOver())
            TicTacToeController.instance.RandomMove(opponent);
    }
}
