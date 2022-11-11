using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    private int gameScore;
    private TextMeshProUGUI textPlayerScore;

    //Gathers all the GameObjects references it needs to work
    private void Start()
    {
        textPlayerScore = GetComponent<TextMeshProUGUI>();
        textPlayerScore.text = "Start";
    }

    //Increases how much score the player currently have
    public void IncreaseScore(int scorePoints)
    {
        this.gameScore += scorePoints;
        textPlayerScore.text = $"{this.gameScore}";
    }
}
