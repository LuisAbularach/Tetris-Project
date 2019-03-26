using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Vector2Int
{
    public int x;
    public int y;


    public Vector2Int(int X, int Y)
    {
        x = X;
        y = Y;

    }
    public Vector2Int(float X, float Y)
    {
        x = Mathf.RoundToInt(X);
        y = Mathf.RoundToInt(Y);

    }
}

public class GameMaster : MonoBehaviour
{
    public static GameMaster current;
    public GameObject percistentScore;

    public Spawner s;
    public Vector2Int GridSize;
    public Transform[,] Grid;

    public GameObject[] Tetrimino;
    public GameObject[] Tetrimino_ns;
    public GameObject[] Text;
    public int min, max, height;
    int NextTetrimino;
    bool isFalling;
    int nextlevel;
    bool isGameOver = false;
    int playerScore;

    public GameObject currentTetriminoFalling = null;
    public GameObject TetriminoNext = null;

    // Start is called before the first frame update
    void Start()
    {
        nextlevel = 5;
        coolOffTime = 0.0f;
        isFalling = true;
        NextTetrimino = GetRand();
        //In play
        if(NextTetrimino != 3)
            currentTetriminoFalling = GameObject.Instantiate(Tetrimino[NextTetrimino], new Vector3(5.0f, height, 0), Quaternion.identity);
        else
            currentTetriminoFalling = GameObject.Instantiate(Tetrimino[NextTetrimino], new Vector3(5.5f, height+0.5f, 0), Quaternion.identity);
        currentTetriminoFalling.GetComponent<Tetrimino>().isFalling = true;
        //next
        Text[2].GetComponent<ScoreHandler>().addPoints(1);
        setnext(0);

    }

    void Awake()
    {
        if (current != null)
            Destroy(gameObject);
        else
            current = this;

        Grid = new Transform[GridSize.x, GridSize.y];

    }

    public bool line(int y)
    {
        for (int i = 0; i < GridSize.x; i++)
        {
            if (Grid[i, y] == null)
                return false;
        }
        Debug.Log(y + " is line");

        return true;
    }

    public bool isInside(int x, int y)
    {
        if (x >= 0 && y >= 0 && y < GridSize.y && x < GridSize.x)
        {
            return true;
        }
        return false;

    }
    public bool inGrid(float x, float y)
    {
        return isInside(Mathf.RoundToInt(x), Mathf.RoundToInt(y));

    }

    public void ByeLine(int y)
    {
        for (int i = 0; i < GridSize.x; i++)
        {
            Destroy(Grid[i, y].gameObject);
        }
        Lower(y + 1);
    }

    void Lower(int y)
    {
        if (y >= GridSize.y)
            return;

        for (int i = 0; i < GridSize.x; i++)
        {
            if (Grid[i, y] != null)
            {
                Grid[i, y].position += Vector3.down;
                Grid[i, y - 1] = Grid[i, y];
                Grid[i, y] = null;
            }
        }
        Lower(y + 1);


    }

    public void Revision()
    {
        int linesDeleted = 0;
        for (int i = GridSize.y-3; i >= 0 ; i--)
        {
        
            Debug.Log("Checking Line " + i + " ");
            if (line(i))
            {
               
                Debug.Log("Line " + i + " is  complete");
                ByeLine(i);
                //need to check row again
                linesDeleted++;
            }
        }
        //Give points and add lines
        Text[0].GetComponent<ScoreHandler>().calculatePoints(linesDeleted);
        percistentScore.GetComponent<ScoreHandler>().calculatePoints(linesDeleted);
        Text[1].GetComponent<ScoreHandler>().addPoints(linesDeleted);
        if (Text[1].GetComponent<ScoreHandler>().getScore() > nextlevel && Text[1].GetComponent<ScoreHandler>().getScore() != 0)
        {
            Text[2].GetComponent<ScoreHandler>().addPoints(1);
            nextlevel = nextlevel + 5;
        }

        
        //Check if any block is on top
        if(GameOver(20)){
            playerScore =  Text[0].GetComponent<ScoreHandler>().getScore();
            DontDestroyOnLoad(percistentScore);
            GameObject music = GameObject.FindGameObjectWithTag("music");
            Destroy(music);
            SceneManager.LoadScene("GameOver");

        }

    }

    public bool GameOver(int top){
        int i;
        for (i = 0; i < GridSize.x; i++)
        {
            if (Grid[i, top] != null)
                return true;
        }
        Debug.Log( i + " has block GAME OVER");

        return false;
    }

    // we need to have some sort of an event to trigger the
    // next tetrimino ...

    // we need to have some sort of a timer ...
    public float speed = 1.0f;
    public float coolOffTime;



    // Update is called once per frame
    void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESCAPE");
            currentTetriminoFalling.GetComponent<Tetrimino>().pause();
        }

    }

    public void OnBoard (int position)
    {
        float xPoint;
        float yPoint = height;
        Debug.Log("START");

        Destroy(TetriminoNext);
        if(NextTetrimino == 3){
            xPoint = 5.5f;
            yPoint -= 0.5f;
        }else{
            xPoint = 5.0f;
        }

        currentTetriminoFalling = GameObject.Instantiate(Tetrimino[NextTetrimino], new Vector3(xPoint , yPoint, 0), Quaternion.identity);
        
        currentTetriminoFalling.GetComponent<Tetrimino>().isFalling = true;


    }

    public void setnext(int position)
    {
        NextTetrimino = GetRand();
        
        Destroy(TetriminoNext);
        Debug.Log("Next Tet piece next: " + NextTetrimino);
        //TetriminoNext = GameObject.Instantiate(Tetrimino_ns[NextTetrimino], new Vector3(14.39f, 1.70f, 0.1f), Quaternion.identity);

        switch (NextTetrimino)
        {
            case 0:
                Place(NextTetrimino, new Vector3(15.15f, 1.00f, 0.1f));
                break;
            case 1:
                Place(NextTetrimino, new Vector3(15.10f, 1.70f, 0.1f));
                break;
            case 2:
                Place(NextTetrimino, new Vector3(14.39f, 1.70f, 0.1f));
                break;
            case 3:
                Place(NextTetrimino, new Vector3(14.39f, 1.70f, 0.1f));
                break;
            case 4:
                Place(NextTetrimino, new Vector3(14.20f, 1.90f, 0.1f));
                break;
            case 5:
                Place(NextTetrimino, new Vector3(15.10f, 2.80f, 0.1f));
                break;
            case 6:
                Place(NextTetrimino, new Vector3(15.39f, 1.70f, 0.1f));
                break;
        }
    }

    int GetRand()
    {
        return  Random.Range(0, 7 );
    }
    
    public void Place(int t, Vector3 pos)
    {
        Debug.Log("START NEXTS " + t);
        TetriminoNext = GameObject.Instantiate(Tetrimino_ns[t], pos, Quaternion.identity);
    }

    void OnDisable()
    {
    PlayerPrefs.SetInt("score", playerScore);
    }

}

