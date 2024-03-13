using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeController : MonoBehaviour
{
    public static TicTacToeController instance;
    
    GridState[,] grid;

    [HideInInspector]public GridState playerChoose = GridState.PLAYER1;
    bool isInGame = true;
    WinLoseState currentWinLose = WinLoseState.NONE;
    [HideInInspector] public int[] score;
    

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
        //退出游戏
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (playerChoose == GridState.NONE)
        {
            throw new ArgumentException("playerChoose suppose to be one of the player but is NONE");
        }
    }

    /*
     *判断胜负来给计分列表加分
     */
    private void FixedUpdate()
    {
        if (currentWinLose == WinLoseState.PLAYER1WIN)
        {
            score[0] += 1;
            isInGame = false;
            Time.timeScale = 0;
        }
        else if (currentWinLose == WinLoseState.PLAYER2WIN)
        {
            score[1] += 1;
            isInGame = false;
            Time.timeScale = 0;
        }
        else if (currentWinLose == WinLoseState.DRAW)
        {
            isInGame = false;
            Time.timeScale = 0;
        }
    }

    public GridState GetGrid(int row, int col)
    {
        return grid[row, col];
    }

    public bool isGameOver()
    {
        if (currentWinLose != WinLoseState.NONE)
            return true;

        return false;
    }

    /*
     * 重置棋盘
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
        Time.timeScale = 1;
        isInGame = true;

        if (playerChoose == GridState.PLAYER2)
        {
            RandomMove(GridState.PLAYER1);
        }
    }

    /*
     * 记录棋盘动作,如果当前格上已经有记录了则不执行任何操作
     */
    public void Move(int row, int col, GridState player)
    {
        if (grid[row, col] == GridState.NONE && isInGame)
        {
            grid[row, col] = player;
            if (currentWinLose == WinLoseState.NONE)
            {
                currentWinLose = CheckWinState(row, col, player);
            }
        }
    }

    /*
     * 每次move后进行一次胜负判定，如果这个格子的同一行，同一列，或者斜边有三个同样的数的情况下，判定玩家胜负，
     * 返回WinLoseState以代表是哪个玩家获胜，如果没有则返回WinLoseState.NONE
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

        //判断同行是否相等
        if (grid[row, 0] == currentGridState && grid[row, 1] == currentGridState && grid[row, 2] == currentGridState)
            return currentPlayer;

        //判断同列是否相等
        if (grid[0, col] == currentGridState && grid[1, col] == currentGridState && grid[2, col] == currentGridState)
            return currentPlayer;

        //如果row + col为偶数，则该格在3x3棋盘的对角线上
        //  判断对角线是否相等
        if ((row + col) % 2 == 0)
        {
            if (grid[0, 0] == currentGridState && grid[1, 1] == currentGridState && grid[2, 2] == currentGridState)
                return currentPlayer;

            if (grid[0, 2] == currentGridState && grid[1, 1] == currentGridState && grid[2, 0] == currentGridState)
                return currentPlayer;
        }

        //如果棋盘里还有没下的格子，说明对局还在继续
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i,j] == GridState.NONE)
                    return WinLoseState.NONE;
            }
        }

        //如果棋盘没有还没下的格子，则本局平局
        return WinLoseState.DRAW;
    }

    /*
     * 指定一个玩家，随机在棋盘上的空格下一步棋
     */
    public void RandomMove(GridState player) 
    {
        var possibleGrid = new List<int[]>();

        //寻找当前的空格
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (grid[i, j] == GridState.NONE)
                {
                    possibleGrid.Add(new int[] { i, j });
                }
            }
        }

        //如果还有空格，随机选择一个空格
        if (possibleGrid.Count != 0)
        {
            var opponentMove = possibleGrid[UnityEngine.Random.Range(0, possibleGrid.Count)];
            Move(opponentMove[0], opponentMove[1], player);
        }
    }

    public void ChoosePlayer1() 
    {
        playerChoose = GridState.PLAYER1;
        ClearGrid();
        score = new int[]{0,0};
    }

    public void ChoosePlayer2()
    {
        playerChoose = GridState.PLAYER2;
        ClearGrid();
        score = new int[] { 0, 0 };
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
