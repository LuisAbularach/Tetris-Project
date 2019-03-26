using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    int playerScore;
    public GameObject score;

    void OnEnable()
    {
    playerScore  =  PlayerPrefs.GetInt("score1");
    score.GetComponent<ScoreHandler>().addPoints(playerScore);
    }
    void Start()
    {
    }



}
