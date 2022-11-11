using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private int gameScore;

    //Increases how much score the player currently have
    public void IncreaseScore(int scorePoints)
    {
        this.gameScore += scorePoints;
        Debug.Log($"Score is now: {this.gameScore}");
    }
}
