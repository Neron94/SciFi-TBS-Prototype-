using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visual_contact : MonoBehaviour {

    Battle_Controller battle_controller;
    public int distanceToContact;


    private void Start()
    {
        battle_controller = transform.GetComponentInParent<Battle_Controller>();
    }


    public unit_operator EnemyContact(unit_operator contestUnit)
    {
        foreach(unit_operator enemy_unit in battle_controller.unit_list.enemyUnitList)
        {
            

        }
        return null;
    }
}
