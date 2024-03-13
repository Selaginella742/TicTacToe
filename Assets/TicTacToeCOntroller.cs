using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeController : MonoBehaviour
{
    public static TicTacToeController instance;
    
    GridState[,] grid;

    bool isInGame = true;
    WinLoseState currentWinLose = WinLoseState.NONE;
    [HideInInspector]public int[] score;
    

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        grid = new GridState[3, 3];
        ClearGrid();

        score = new int[] { 0, 0 };
        isInGame = true;
    }

    void Update()
    {
        
    }

    public GridState GetGrid(int row, int col)
    {
        return grid[row, col];
    }

    /*
     * ��������
     */
    public void ClearGrid()
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = GridState.NONE;
            }
        }
        currentWinLose = WinLoseState.NONE;
    }

    /*
     * ��¼���̶�����������ʤ����� �����ΪGridState.NONE,��Ϊ��û��ʤ����
     */
    public void Move(int row, int col, GridState player)
    {
        grid[row, col] = player;
        currentWinLose = CheckWinState(row, col, player);
    }

    /*
     * ÿ��move�����һ��ʤ���ж������������ӵ�ͬһ�У�ͬһ�У�����б��������ͬ������������£��ж����ʤ����
     * ����WinLoseState�Դ������ĸ���һ�ʤ�����û���򷵻�
     */
    public WinLoseState CheckWinState(int row, int col, GridState player)
    {
        var currentGridState = grid[row, col];

        var currentPlayer = WinLoseState.NONE;

        if (player == GridState.PLAYER1)
        {
            currentPlayer = WinLoseState.PLAYER1WIN;
        }
        else if (player == GridState.PLAYER2)
        {
            currentPlayer = WinLoseState.PLAYER2WIN;
        }

        //�ж�ͬ���Ƿ����
        if (grid[row, 0] == currentGridState && grid[row, 1] == currentGridState && grid[row, 2] == currentGridState)
            return currentPlayer;

        //�ж�ͬ���Ƿ����
        if (grid[0, col] == currentGridState && grid[1, col] == currentGridState && grid[2, col] == currentGridState)
            return currentPlayer;

        //���row + colΪż������ø���3x3���̵ĶԽ�����
        //  �ж϶Խ����Ƿ����
        if ((row + col) % 2 == 0)
        {
            if (grid[0, 0] == currentGridState && grid[1, 1] == currentGridState && grid[2, 2] == currentGridState)
                return currentPlayer;

            if (grid[0, 2] == currentGridState && grid[1, 1] == currentGridState && grid[2, 0] == currentGridState)
                return currentPlayer;
        }

        //��������ﻹ��û�µĸ��ӣ�˵���Ծֻ��ڼ���
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i,j] == GridState.NONE)
                    return WinLoseState.NONE;
            }
        }

        //�������û�л�û�µĸ��ӣ��򱾾�ƽ��
        return WinLoseState.DRAW;
    }
}

public enum GridState 
{
    NONE,
    PLAYER1,
    PLAYER2
}

public enum WinLoseState
{
    NONE,
    PLAYER1WIN,
    PLAYER2WIN,
    DRAW
}
