using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate_System_Operator : MonoBehaviour {


    List<Square_cell_Operator> Open_List;
    List<Square_cell_Operator> Close_List;


    public List<Square_cell_Operator> Find_path(Square_cell_Operator start, Square_cell_Operator end)
    {
        if(Close_List.Count == 0)
        {
            SquareCheckToOpenList(start);
        }
        else
        {
            if(Open_List.Count != 0)
            {
                SquareCheckToOpenList(SearchInOpen_listNextSquare());
            }
        }
        return null;
    }

    void SquareCheckToOpenList(Square_cell_Operator CheckSquare)
    {
        foreach(Square_cell_Operator checkingSquare in CheckSquare.Around_Squares)
        {
            if (!checkingSquare.barrier)
            {
                if (!isCheckInClose_List(CheckSquare))
                {
                   
                }
                else;   // Proverochniy Square uje v CloseList


            }
            else;   //Proverochniy Square imeet barier
            
        }
    }

    bool isCheckInOpen_List(Square_cell_Operator checkingSquare)
    {
        foreach(Square_cell_Operator CheckSq in Open_List)
        {
            if(CheckSq == checkingSquare)
            {
                return true;
            }     
        }
        return false;
    }
    bool isCheckInClose_List(Square_cell_Operator checkingSquare)
    {
        foreach (Square_cell_Operator CheckSq in Close_List)
        {
            if (CheckSq == checkingSquare)
            {
                return true;
            }
        }
        return false;
    }

    void Path_finding_cycle(Square_cell_Operator start)
    {

    }

    bool isEndinOpen_List()
    {
        return false;
    }

    List<Square_cell_Operator> Calculate_Path(Square_cell_Operator end)
    {

    }

    Square_cell_Operator SearchInOpen_listNextSquare()
    {
        int best_F = Open_List[0].A_Value[0];
        Square_cell_Operator bestChoice = Open_List[0];

        foreach (Square_cell_Operator checkingSquare in Open_List)
        {
            

            if(checkingSquare.A_Value[0] < best_F)
            {
                best_F = checkingSquare.A_Value[0];
                bestChoice = checkingSquare;
            }
           
        }
        return bestChoice;
    }

    
}
