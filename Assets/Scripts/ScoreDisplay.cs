using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text scoreToDisplay;
    void Start()
    {
        scoreToDisplay = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var player1Socre = TicTacToeController.instance.score[0];
        var player2Socre = TicTacToeController.instance.score[1];
        scoreToDisplay.text = (player1Socre + "   :   " + player2Socre);
    }
}
