using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_List : MonoBehaviour {

    public List<unit_operator> playerUnitList = new List<unit_operator>();
    public List<unit_operator> enemyUnitList = new List<unit_operator>();


    public void RestoreAP(int playerOrEnemy)
    {
        switch(playerOrEnemy)
        {
            case 1: //Player Start turn
                foreach(unit_operator playerUnit in playerUnitList)
                {
                    playerUnit.RestoreAp();
                }
                break;
            case 2: // Enemy Start turn
                foreach (unit_operator enemyUnit in enemyUnitList)
                {
                    enemyUnit.RestoreAp();
                }
                break;
        }
    }

}
