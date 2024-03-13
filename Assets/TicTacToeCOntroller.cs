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
        //�˳���Ϸ
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
     *�ж�ʤ�������Ʒ��б�ӷ�
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
        Time.timeScale = 1;
        isInGame = true;

        if (playerChoose == GridState.PLAYER2)
        {
            RandomMove(GridState.PLAYER1);
        }
    }

    /*
     * ��¼���̶���,�����ǰ�����Ѿ��м�¼����ִ���κβ���
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
     * ÿ��move�����һ��ʤ���ж������������ӵ�ͬһ�У�ͬһ�У�����б��������ͬ������������£��ж����ʤ����
     * ����WinLoseState�Դ������ĸ���һ�ʤ�����û���򷵻�WinLoseState.NONE
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

    /*
     * ָ��һ����ң�����������ϵĿո���һ����
     */
    public void RandomMove(GridState player) 
    {
        var possibleGrid = new List<int[]>();

        //Ѱ�ҵ�ǰ�Ŀո�
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

        //������пո����ѡ��һ���ո�
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
