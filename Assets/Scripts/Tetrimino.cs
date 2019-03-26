using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TetriminoType
{
    I, J, L, O, S, T, Z
};

public class Tetrimino : MonoBehaviour
{
    public TetriminoType Type;
    public bool isFalling = false;
    public bool active = true;
    bool dropped;
    int countwall;
    bool onleft;
    GameMaster g;


    [Tooltip("Used for translation")]
    public GameObject Root;

    [Tooltip("Used for rotation")]
    public GameObject Pivot;

    [SerializeField]
    protected GameObject[] cubes = new GameObject[4];

    public bool paused = false;

    public void pause()
    {
        Debug.Log("PAUSING");
        if (!paused)
            paused = true;
        else
            paused = false;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {            
        g = GameMaster.current;
        dropped = false;
        countwall = 0;
        while (true)
        {
            if (paused == false)
            {
                if (dropped)
                {
                    dropped = true;
                    //Move down until we hit block
                    while (allGood())
                    {
                        transform.position += Vector3.down;
                    }
                    //move up one
                    transform.position -= Vector3.down * 2.0f;
                    dropped = false;

                }
                transform.position += Vector3.down;
            }


            if (allGood())
            {
                GridUpdate();
            }
            else
            {
                transform.position -= Vector3.down;
                active = false;
                g.Revision();
                //Debug.Log("NEW TETERIS OBJECT");
                int tet = Random.Range(0, 6);
                g.OnBoard(tet);
                g.setnext(tet);
                break;
            }
 

            yield return new WaitForSeconds(.5f);
        }
        Debug.Log("peice set");
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)&&!paused)
            {
                transform.position += Vector3.left;
                if (allGood())
                {
                    GridUpdate();
                }
                else
                {
                    transform.position -= Vector3.left;
                }


            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && !paused)
            {
                transform.position += Vector3.right;
                if (allGood())
                {
                    GridUpdate();
                }
                else
                {
                    transform.position -= Vector3.right;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && !paused)
            {
                transform.Rotate(0, 0, 90);
                if (allGood())
                {
                    GridUpdate();
                }
                else
                {
                    //Check what side the grid is filled
                    if (HitLeft())
                        onleft = true;
                    else
                        onleft = false;

                    Debug.Log("On Left is " + onleft); 
                    while (!allGood())
                    {
                        countwall++;
                       
                        if (onleft)
                        {
                            transform.position += Vector3.right;
                        }

                        else
                        {
                            transform.position += Vector3.left;
                        }

                    }
                    //transform.Rotate(0, 0, -90);
                    countwall = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && !paused)
            {
                transform.Rotate(0, 0, -90);
                if (allGood())
                { 
                    GridUpdate();
                }
                else
                {
                    if (HitLeft())
                        onleft = true;
                    else
                        onleft = false;

                    if(betweenBlocks((int)transform.position.x, (int)transform.position.y)){
                        transform.Rotate(0,0,90);
                    }
                    else{ 
                        Debug.Log("On Left is " + onleft);
                        while (!allGood())
                        {
                            countwall++;
                            //Check what side the grid is filled
                            if (onleft)
                            {
                                transform.position += Vector3.right;
                                Debug.Log("NO!");
                            }

                            else
                            {
                                Debug.Log("YES!");
                                transform.position += Vector3.left;
                            }

                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && !paused)
            {
                Debug.Log("drop!!");
                dropped = true;
            }

        }
    }

    bool insideGrid()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform t = transform.GetChild(i);
            if (!g.inGrid(t.position.x, t.position.y))
                return false;
        }

        return true;
    }

    bool allGood()
    {
        if (!insideGrid())
        {
            Debug.Log("Piece not in grid ");
            return false;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform t = transform.GetChild(i);
            Vector2Int v = new Vector2Int(t.position.x, t.position.y);
            if (g.Grid[v.x, v.y] != null)
            {
                if (g.Grid[v.x, v.y].parent != transform)
                {
                    return false;
                }

            }
        }

        return true;

    }


    void GridUpdate()
    {
        for (int i = 0; i < g.GridSize.x; i++)
        {
            for (int j = 0; j < g.GridSize.y; j++)
            {
                if (g.Grid[i, j] != null)
                {
                    if (g.Grid[i, j].parent == transform)
                    {
                        g.Grid[i, j] = null;
                    }
                }
            }
        }


        for (int i = 0; i < transform.childCount; i++)
        {
            Transform t = transform.GetChild(i);
            Vector2Int v = new Vector2Int(t.position.x, t.position.y);
            g.Grid[v.x, v.y] = t;
        }


    }

    bool HitLeft()
    {
        int x = (int)transform.position.x;
        int y = (int)(int)transform.position.y;
         
        if (x - 1 < 0  || x - 2 < 0 || x - 3 < 0)
        {
            Debug.Log("WALL");
            //Wall
            return true;
        }
        
        if(x + 1 > 9|| x + 2 > 9 || x + 3 > 9)
        {
            //Right Wall
            Debug.Log("Right Wall");
            return false;
        }

        //  if (g.Grid[(int)transform.position.x - 2, (int)transform.position.y] != null &&
        //     g.Grid[(int)transform.position.x - 3, (int)transform.position.y] != null)
        // {
        //     Debug.Log("Block");
        //     //Blocks in the way
        //     return true;
        // }

        if (checkPosition(x-1,y) || checkPosition(x-2,y) || checkPosition(x-3,y))
        {
            Debug.Log("Block");
            //Blocks in the way
            return true;
        }
        return false;
    }

    bool checkPosition(int x, int y){
        bool Good= false;
        for(int i = 0; i < transform.childCount; i++ ){
            if( g.Grid[x,y]!=null || g.Grid[x,y].position!=transform.GetChild(i).position){
                Good = true;
            }
        }

        return Good;
    }



    bool betweenBlocks(int x, int y)
    {
        //check if between blocks
        if((checkPosition(x+1,y) || checkPosition(x+2,y) || checkPosition(x+3,y)) && 
        (checkPosition(x-1,y) || checkPosition(x-2,y) || checkPosition(x-3,y)))
        {
            return true;
        }
        return false;
    }

}