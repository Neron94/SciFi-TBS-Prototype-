using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_squad : MonoBehaviour {

    public Core_AI core_ai;
    public List<ai_unit> all_unit = new List<ai_unit>();
    List<Square_cell_Operator> squares = new List<Square_cell_Operator>(); // Cover Squares
    List<bool> squadMoveFinished = new List<bool>();
    public bool isTurnEnd = false;


    private void Start()
    {
        core_ai = GameObject.Find("Core_AI").GetComponent<Core_AI>();
        core_ai.all_squad.Add(this);
        
    }

    public void Order()
    {
        Defence();
    }

    void Defence()
    {
        WhereNearEnemyContact();
        SearchForCover();
    }

    void WhereNearEnemyContact()
    {
        foreach(ai_unit unit in all_unit)
        {
            unit_operator uOperator = unit.GetComponent<unit_operator>();
            float bestDistance = 0;
            foreach(unit_operator enemy in core_ai.BC.unit_list.playerUnitList)
            {
                if(enemy.hp != 0)
                {
                    if (bestDistance == 0)
                    {
                        bestDistance = Vector3.Distance(uOperator.transform.position, enemy.transform.position);
                        unit.nearEnemy = enemy;
                    }

                    if (Vector3.Distance(uOperator.transform.position, enemy.transform.position) < bestDistance)
                    {
                        unit.nearEnemy = enemy;
                        bestDistance = Vector3.Distance(uOperator.transform.position, enemy.transform.position);
                    }
                }
                
            }
        }
    }

    void SearchForCover()
    {
        squares.Clear();
        foreach(Square_cell_Operator square in core_ai.FC._Field_List())
        {
            if(square.barrier)
            {
                squares.Add(square);
            }
        }
        foreach(ai_unit unit in all_unit)
        {
            unit_operator uOperator = unit.GetComponent<unit_operator>();
            float bestDistance = 0;
            
            foreach(Square_cell_Operator covers in squares)
            {
                
                    if (bestDistance == 0)
                    {
                        unit.coverPoint = covers;
                        bestDistance = Vector3.Distance(uOperator.transform.position, covers.transform.position);
                    }

               
                    if (Vector3.Distance(uOperator.transform.position, covers.transform.position) < bestDistance && !core_ai.HaveUnitInCover(covers))
                    {
                        unit.coverPoint = covers;
                        bestDistance = Vector3.Distance(uOperator.transform.position, covers.transform.position);
                    }
                    else if(Vector3.Distance(uOperator.transform.position, covers.transform.position) >= bestDistance && !core_ai.HaveUnitInCover(covers))
                    {
                    unit.coverPoint = covers;
                    bestDistance = Vector3.Distance(uOperator.transform.position, covers.transform.position);
                    }

            }
            
        }
    }

    


    public void SetOrderToUnit()
    {
        if (unitActiveTest())
        {
            foreach (ai_unit unit in all_unit)
            {
                if (!unit.isTurnEnd)
                {
                   
                        unit.Movement(unit.WhereToCover()[0]);
                        break;
                    
                }
            }
        }
        else
        {
            isTurnEnd = true;
            core_ai.SetOrderToSquad();
        }
        
    }

    bool unitActiveTest()
    {
        foreach(ai_unit unit in all_unit)
        {
            if(unit.isTurnEnd == false)
            {
                return true;
            }
        }
        return false;
    }


}
