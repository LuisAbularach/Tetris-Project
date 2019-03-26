using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITetrimino
{
    TetriminoType GetType();

     void place(Vector3 position);

    //void Rotation(bool left);
}