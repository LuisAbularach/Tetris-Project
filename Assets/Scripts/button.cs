using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    public bool isScoreScene;
    GameObject score = null;
    public void changeToGame(string SceneName)
    {   
        if(isScoreScene){
            Debug.Log("SCORE1");
            score = GameObject.FindGameObjectWithTag("score");
        }
        if(score != null)
            Destroy(score);
        SceneManager.LoadScene(SceneName);
    }

    public void toggleMusic(bool isOn){
        
    }
}
