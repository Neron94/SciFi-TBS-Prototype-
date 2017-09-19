using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visual_contact : MonoBehaviour {

    Battle_Controller battle_controller;
    public int distanceToContact = 5;
    List<unit_operator> contactList = new List<unit_operator>();


    private void Start()
    {
        battle_controller = transform.GetComponentInParent<Battle_Controller>();
    }
    public void EnemyContact(unit_operator contestUnit)
    {
        contactList.Clear();
        foreach(unit_operator enemy_unit in battle_controller.unit_list.enemyUnitList)
        {
            if(battle_controller.distance_calculator.Distance(contestUnit, enemy_unit) < distanceToContact)
            {
                enemy_unit.Detected();
                contactList.Add(enemy_unit);
            }

        }
        
    }
    public void ClearContact()
    {
        foreach(unit_operator unit in contactList)
        {
            unit.Detected();
        }
        
    }

    public bool Visual_Contact(unit_operator myUnit, unit_operator enemy)
    {
        Ray unitEyeRay = new Ray(myUnit.Eyes_Object.transform.position, enemy.Eyes_Object.transform.position-myUnit.Eyes_Object.transform.position);
        Debug.DrawRay(myUnit.Eyes_Object.transform.position, enemy.Eyes_Object.transform.position - myUnit.Eyes_Object.transform.position);
        RaycastHit hit;
        Physics.Raycast(unitEyeRay, out hit);
        GameObject detected_obj = hit.collider.gameObject;

        if(detected_obj.tag == "enemy")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
