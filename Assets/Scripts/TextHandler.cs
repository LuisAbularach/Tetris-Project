using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreHandler : MonoBehaviour
{
    private TextMeshPro text;
    int thisScore;
    // Start is called before the first frame update
    void Start()
    {
        //Start at 0
        text = GetComponent<TextMeshPro>();
        thisScore = 0;

    }
    public int getScore()
    {
        return thisScore;
    }


    public void addPoints(int value)
    {
        thisScore = thisScore + value;
        text.text = thisScore.ToString();
        ;
    }

    

    public void calculatePoints(int lines)
    {
        switch (lines)
        {
            case 1:
                addPoints(40);
                break;
            case 2:
                addPoints(100);
                break;
            case 3:
                addPoints(300);
                break;
            case 4:
                addPoints(1200);
                break;
            default:
                addPoints(0);
                break;
        }
            
    }
}
