using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_AI : MonoBehaviour {

    public List<ai_squad> all_squad = new List<ai_squad>();
    public Field_Controller FC;
    public Battle_Controller BC;
    private void Start()
    {
        FC = GameObject.Find("Field_Controller").GetComponent<Field_Controller>();
        BC = GameObject.Find("Battle_Controller").GetComponent<Battle_Controller>();
    }


    public void Start_AI()
    {
        RestoreTurns();
       SetOrderToSquad();
    }

    public void SetOrderToSquad()
    {
        if(squadActiveTest())
        {
            foreach (ai_squad squad in all_squad)
            {
                if (!squad.isTurnEnd)
                {
                    squad.Order();
                    squad.SetOrderToUnit();
                    break;
                }
            }
        }
        else
        {
            print("ИИ закончил Ход");
        }
        
        
    }

    bool squadActiveTest()
    {
        foreach(ai_squad squad in all_squad)
        {
            if(squad.isTurnEnd == false)
            {
                return true;
               
            }
        }
        return false;
    }

    void RestoreTurns()
    {
        foreach(ai_squad squads in all_squad)
        {
            squads.isTurnEnd = false;

            foreach(ai_unit unit in squads.all_unit)
            {
                unit.isTurnEnd = false;
            }
        }
    }

    public bool HaveUnitInCover(Square_cell_Operator square)
    {
        foreach (ai_squad squads in all_squad)
        {
            foreach (ai_unit unit in squads.all_unit)
            {
                if (unit.coverPoint == square)
                {
                    return true;
                }
            }
        }
        return false;
    }

}
