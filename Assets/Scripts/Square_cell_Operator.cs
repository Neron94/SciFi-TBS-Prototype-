using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square_cell_Operator : MonoBehaviour {

    int[] square_id = new int[2];
    Transform square_Pos;
    public bool barrier;
    public int[] A_Value = new int [3];   //Значение F G H
    public Square_cell_Operator Parent;
    public List<Square_cell_Operator> Around_Squares;


    private void Start()
    {
        
    }

    public void SetCoordinate(int x, int y)
    {
        square_id[0] = x;
        square_id[1] = y;
    }

    public int[] GetCoordinates()
    {
        return square_id;
    }

    void Set_Around_squares()
    {
       
    }
}
