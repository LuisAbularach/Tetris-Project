using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextHandler : MonoBehaviour
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
}
 
