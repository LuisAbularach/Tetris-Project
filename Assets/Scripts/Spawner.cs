using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
	public GameObject[] Tetrimino;
    public GameObject[] Tetrimino_ns;
    GameObject next, current;
    int NextTetrimino;


    void Start()
	{
        //First Tetrimino
        NextTetrimino = Random.Range(0, 6);
        initialize(NextTetrimino);
        //Get the second Tetrimino
        //NextTetrimino = Random.Range(0, Tetrimino.Length);
    }

	public void initialize(int position)
	{
        if (next != null)
        {
            //Detroy next
            Destroy(next);
        }
        //Spawn one of 7 prefab shapes on board
        //Debug.Log("Next Tet piece: " + NextTetrimino);
        current = (GameObject) Instantiate (Tetrimino[NextTetrimino], transform.position, Quaternion.identity);
        

    }

    public void SetNext(int position)
    {
        NextTetrimino = Random.Range(0, 6);
        Debug.Log("Next Tet piece next: " + position);
        next = (GameObject)Instantiate(Tetrimino_ns[NextTetrimino], new Vector3(0, 0, 0), Quaternion.identity);
    }

}
