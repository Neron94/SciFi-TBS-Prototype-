using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAvailableMoves : MonoBehaviour {

    Field_Controller FC;
    public List<Square_cell_Operator> ShowingSquaresMax = new List<Square_cell_Operator>();
    public List<Square_cell_Operator> ShowingSquaresMin = new List<Square_cell_Operator>();


    private void Start()
    {
        FC = transform.GetComponentInParent<Field_Controller>();
    }
    public List<Square_cell_Operator> Show (string id, int min, int max, Square_cell_Operator start)
    {
        switch (id)
        {
            case "min":
                ShowingSquaresMin.Clear();
                ShowingSquaresMax.Clear();
                return ShowMin(min,start);
                
            case "max":
                return ShowMax(max, start, ShowingSquaresMin);
                
        }
        return null;
    }


     List<Square_cell_Operator> ShowMin(int Min, Square_cell_Operator startPoint)
    {

       /* ShowingSquaresMin.Clear();
        ShowingSquaresMax.Clear(); // CLEARIINGGGGGG*/


        List<Square_cell_Operator> operationList = new List<Square_cell_Operator>();

        foreach (Square_cell_Operator sqr in startPoint.Around_Squares)
        {
            if(!ShowingSquaresMin.Contains(sqr) && !sqr.barrier && !sqr.haveUnitOn) // If almost have no this Square in List
            {
                ShowingSquaresMin.Add(sqr);
                operationList.Add(sqr);
            }
            
        }

        for (int i = 1; i < Min; i++)
        {
            for (int x = 0; x < operationList.Count; x++)
            {
                foreach (Square_cell_Operator square in ShowingSquaresMin[x].Around_Squares)
                {
                    if(!ShowingSquaresMin.Contains(square) && !square.barrier && !square.haveUnitOn)
                    {
                        ShowingSquaresMin.Add(square);
                    }
                }
            }

            foreach (Square_cell_Operator squareFromOL in ShowingSquaresMin)
            {
                if (!operationList.Contains(squareFromOL) && !squareFromOL.barrier && !squareFromOL.haveUnitOn) operationList.Add(squareFromOL);
            }
        }





        return ShowingSquaresMin;
    } 
     List<Square_cell_Operator> ShowMax( int Max, Square_cell_Operator startPoint, List<Square_cell_Operator> MinList)
    {
        
        List<Square_cell_Operator> operationList = new List<Square_cell_Operator>();

        foreach (Square_cell_Operator sqr in startPoint.Around_Squares)
        {
            if (!ShowingSquaresMax.Contains(sqr) && !sqr.barrier && !sqr.haveUnitOn) // If almost have no this Square in List
            {
                ShowingSquaresMax.Add(sqr);
                operationList.Add(sqr);
            }

        }

        for (int i = 1; i < Max; i++)
        {
            for (int x = 0; x < operationList.Count; x++)
            {
                foreach (Square_cell_Operator square in ShowingSquaresMax[x].Around_Squares)
                {
                    if (!ShowingSquaresMax.Contains(square) && !square.barrier && !square.haveUnitOn)
                    {
                        ShowingSquaresMax.Add(square);
                    }
                }
            }

            foreach (Square_cell_Operator squareFromOL in ShowingSquaresMax)
            {
                if (!operationList.Contains(squareFromOL) && !squareFromOL.barrier && !squareFromOL.haveUnitOn) operationList.Add(squareFromOL);
            }
        }
        List<Square_cell_Operator> finalList = new List<Square_cell_Operator>();
        finalList.AddRange(ShowingSquaresMax);
        ShowingSquaresMax.CopyTo(finalList.ToArray());

        foreach (Square_cell_Operator squareInMin in MinList)
        {
            foreach (Square_cell_Operator squareToClean in finalList)
            {
                if (MinList.Contains(squareToClean)) ShowingSquaresMax.Remove(squareToClean);
            }
        }
        

        return ShowingSquaresMax;
    }


}
