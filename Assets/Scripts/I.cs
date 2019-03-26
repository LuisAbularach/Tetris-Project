using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I : Tetrimino, ITetrimino
{

    public void Rotation(bool left)
    {
        if(left){
            transform.Rotate(0, 0, 90);
            transform.position += Vector3.up;
        }
    }

    public void place(Vector3 position)
    {
        transform.position = position;
    }

    TetriminoType ITetrimino.GetType()
    {
        return Type;
    }
}