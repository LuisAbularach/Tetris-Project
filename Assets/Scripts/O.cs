using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O : Tetrimino, ITetrimino
{
    // void Start(){
    //     transform.position = new Vector3(5.5f,20.0f);
    // }
     public void place(Vector3 position)
    {
        transform.position = new Vector3(position.x+0.5f,position.y,0.0f);
    }

    TetriminoType ITetrimino.GetType()
    {
        return Type;
    }
}