﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square_cell_Operator : MonoBehaviour {


    Field_Controller FC;
    int[] square_id = new int[2] {1212,1212};
    Transform square_Pos;
    public bool barrier;
    public int[] A_Value = new int [3];   //Значение F G H
    public Square_cell_Operator Parent;
    public List<Square_cell_Operator> Around_Squares = new List<Square_cell_Operator>();

    


    private void Awake()
    {
        FC = GameObject.Find("Field_Controller").GetComponent<Field_Controller>();
        FC.AddSquareToList(this);
    }
    private void Start()
    {
        
        Set_Around_squares();
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
       foreach(Square_cell_Operator square in FC._Field_List())
        {
            if(square.GetCoordinates()[0] == square_id[0] - 1 && square.GetCoordinates()[1] == square_id[1] - 1)
            {
                Around_Squares.Add(square);
            }
            if (square.GetCoordinates()[0] == square_id[0] - 1 && square.GetCoordinates()[1] == square_id[1] + 1)
            {
                Around_Squares.Add(square);
            }
            if (square.GetCoordinates()[0] == square_id[0] + 1 && square.GetCoordinates()[1] == square_id[1] - 1)
            {
                Around_Squares.Add(square);
            }
            if (square.GetCoordinates()[0] == square_id[0] + 1 && square.GetCoordinates()[1] == square_id[1] + 1)
            {
                Around_Squares.Add(square);
            }
            if (square.GetCoordinates()[0] == square_id[0] - 1 && square.GetCoordinates()[1] == square_id[1])
            {
                Around_Squares.Add(square);
            }
            if (square.GetCoordinates()[0] == square_id[0] && square.GetCoordinates()[1] == square_id[1] - 1)
            {
                Around_Squares.Add(square);
            }
            if (square.GetCoordinates()[0] == square_id[0] + 1 && square.GetCoordinates()[1] == square_id[1])
            {
                Around_Squares.Add(square);
            }
            if (square.GetCoordinates()[0] == square_id[0] && square.GetCoordinates()[1] == square_id[1] + 1)
            {
                Around_Squares.Add(square);
            }
        }
    }

    public void SetColor(Color colorToCube)
    {
        GetComponentInChildren<SpriteRenderer>().color = colorToCube;
    }
}
