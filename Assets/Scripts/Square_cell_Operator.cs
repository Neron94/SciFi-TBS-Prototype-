using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square_cell_Operator : MonoBehaviour {

    int[] square_id = new int[2];
    Transform square_Pos;
    bool barrier;


	public void SetCoordinate(int x, int y)
    {
        square_id[0] = x;
        square_id[1] = y;
    }
}
