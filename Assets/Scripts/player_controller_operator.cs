﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller_operator : MonoBehaviour {

    Square_cell_Operator chosenSquare;
    Coordinate_System_Operator CSO;
    Color colBarri = new Color(255,0,0,255);
    GameObject selUnit;

    private void Start()
    {
        CSO = GameObject.Find("Coordinate_System_Manager").GetComponent<Coordinate_System_Operator>();
    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(mouseRay, out hit, 2000);
            GameObject selected_gameob = hit.collider.gameObject;
            

            if (selected_gameob.tag == "square")
            {

                if (selUnit)
                {
                    if (selected_gameob.tag == "square")
                    {
                        //## TODO: Verify isAlmostHaveUnitOnSquare
                        selUnit.GetComponent<unit_operator>().Move(CSO.GetPath(selUnit.GetComponent<unit_operator>().myPos, selected_gameob.GetComponent<Square_cell_Operator>()));
                        selUnit = null;
                        selected_gameob = null;
                    }
                    
                }
                else
                {
                
                }

            }
            //Unit Chosing
            if(selected_gameob != null && selected_gameob.tag == "myUnit")
            {
                selUnit = null;
                selected_gameob.GetComponent<unit_operator>().Select();
                selUnit = selected_gameob;
            }
            if (selUnit != null)
            {
                if(selected_gameob.tag == "enemy")
                {
                    print("Click po Vragu");
                    //#TODO: #Cheking distance >>> #Type of Weapon
                    //#TODO: #Atack result Calculation before striking
                    //#TODO: #Duoble Confirning click to strike >>> #Call function of UnitOperator to deal damage
                }
                
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(mouseRay, out hit, 2000);
            GameObject selected_gameob = hit.collider.gameObject;

            if(selected_gameob.tag == "square")
            {
                selected_gameob.GetComponent<Square_cell_Operator>().SetColor(colBarri);
                selected_gameob.GetComponent<Square_cell_Operator>().barrier = true;
            }

            
        }
    }
}
