using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T : Tetrimino, ITetrimino
{

    public void place(Vector3 position)
    {
        transform.position = position;
    }

    TetriminoType ITetrimino.GetType()
    {
        return Type;
    }
}