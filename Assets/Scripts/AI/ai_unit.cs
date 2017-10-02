using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_unit : MonoBehaviour {

    public Square_cell_Operator coverPoint;
    public unit_operator nearEnemy;
    public unit_operator myUnitOperator;
    public Coordinate_System_Operator CSO;
    ai_squad mySquad;

    private void Start()
    {
        myUnitOperator = transform.GetComponent<unit_operator>();
        mySquad = transform.GetComponent<ai_squad>();
        mySquad.all_unit.Add(this);
        CSO = GameObject.Find("Coordinate_System_Manager").GetComponent<Coordinate_System_Operator>();
    }

    public List<Square_cell_Operator> WhereToCover()
    {
        int coverX = coverPoint.GetCoordinates()[0];
        int coverY = coverPoint.GetCoordinates()[1];

        int enemyX = nearEnemy.myPos.GetCoordinates()[0];
        int enemyY = nearEnemy.myPos.GetCoordinates()[1];

        
        List<Square_cell_Operator> posToCover = new List<Square_cell_Operator>();

        if (coverX > enemyX)
        {
            if(coverY > enemyY)
            {
                print("Verh leviy"); 
                foreach(Square_cell_Operator square in mySquad.core_ai.FC._Field_List())
                {
                    if(coverX == square.GetCoordinates()[0])
                    {
                        if(square.GetCoordinates()[1] == coverY+1)
                        {
                            
                            posToCover.Add(square);
                        }
                    }
                }
            }
            else if(coverY < enemyY)
            {
                print("//VerhPraviy");
                
                foreach (Square_cell_Operator square in mySquad.core_ai.FC._Field_List())
                {
                    if (coverX == square.GetCoordinates()[0])
                    {
                        if (square.GetCoordinates()[1] == coverY-1)
                        {
                            posToCover.Add(square);
                        }
                    }
                }
            }
            else if(coverY == enemyY)
            {
                print("//Verh");
                foreach (Square_cell_Operator square in mySquad.core_ai.FC._Field_List())
                {
                    if (square.GetCoordinates()[0] == coverX+1)
                    {
                        if (coverY == square.GetCoordinates()[1])
                        {
                            posToCover.Add(square);
                        }
                    }
                }
            }
        }
        else if(coverX < enemyX)
        {
            if (coverY > enemyY)
            {
                print("//Niz leviy");
                foreach (Square_cell_Operator square in mySquad.core_ai.FC._Field_List())
                {
                    if (square.GetCoordinates()[0] == coverX-1)
                    {
                        if (coverY == square.GetCoordinates()[1])
                        {
                            posToCover.Add(square);
                        }
                    }
                }

            }
            else if (coverY < enemyY)
            {
                print("//Niz Praviy");
                foreach (Square_cell_Operator square in mySquad.core_ai.FC._Field_List())
                {
                    if (coverX == square.GetCoordinates()[0])
                    {
                        if (square.GetCoordinates()[1] == coverY-1)
                        {
                            posToCover.Add(square);
                        }
                    }
                }
            }
            else if (coverY == enemyY)
            {
                print("//Niz");
                foreach (Square_cell_Operator square in mySquad.core_ai.FC._Field_List())
                {
                    if (square.GetCoordinates()[0] == coverX-1)
                    {
                        if (coverY == square.GetCoordinates()[1])
                        {
                            posToCover.Add(square);
                        }
                    }
                }
            }
        }
        else if(coverX == enemyX)
        {
            if (coverY > enemyY)
            {
                print("//levo");
                foreach (Square_cell_Operator square in mySquad.core_ai.FC._Field_List())
                {
                    if (coverX == square.GetCoordinates()[0])
                    {
                        if (square.GetCoordinates()[1] == coverY+1)
                        {
                            posToCover.Add(square);
                        }
                    }
                }
            }
            else if (coverY < enemyY)
            {
                print("////Pravo");
                foreach (Square_cell_Operator square in mySquad.core_ai.FC._Field_List())
                {
                    if (coverX == square.GetCoordinates()[0])
                    {
                        if (square.GetCoordinates()[1] == coverY-1)
                        {
                            posToCover.Add(square);
                        }
                    }
                }
            }
            
        }
        print(posToCover[0].GetCoordinates()[0]);
        print(posToCover[0].GetCoordinates()[1]);
        return posToCover;


    }

    public void Movement(Square_cell_Operator go)
    {
       if(myUnitOperator.myPos != go)
        {
            List<Square_cell_Operator> path = CSO.GetPath(myUnitOperator.myPos, go);
            if (myUnitOperator.action_point > 0)
            {
                if(path.Count - 1 >= myUnitOperator.stepMinMax[0])
                {
                    myUnitOperator.action_point = 0;
                    myUnitOperator.Move(path);
                }
                else if(path.Count-1 <= myUnitOperator.stepMinMax[0])
                {
                    myUnitOperator.action_point --;
                    myUnitOperator.Move(path);
                }
                else if(path.Count- 1> myUnitOperator.stepMinMax[1])
                {
                    print("путь слишком далек для ОД");
                }
                

            }
        }
        
    }

    void OpenFire()
    {

    }


}
