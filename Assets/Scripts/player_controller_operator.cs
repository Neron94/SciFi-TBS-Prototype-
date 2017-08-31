using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller_operator : MonoBehaviour {

    Square_cell_Operator chosenSquare;
    Coordinate_System_Operator CSO;
    Color colBarri = new Color(255,0,0,255);
    GameObject selUnit;
    UI_Controller UI;

    private void Start()
    {
        CSO = GameObject.Find("Coordinate_System_Manager").GetComponent<Coordinate_System_Operator>();
        UI = GameObject.Find("UI_Controller").GetComponent<UI_Controller>();
    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UI.onUI)
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
                        //If almost no unit in this CUBE
                        if(!selected_gameob.GetComponent<Square_cell_Operator>().haveUnitOn)
                        {
                            selUnit.GetComponent<unit_operator>().Move(CSO.GetPath(selUnit.GetComponent<unit_operator>().myPos, selected_gameob.GetComponent<Square_cell_Operator>()));
                            selUnit = null;
                            selected_gameob = null;
                            UI.ShowStatus(false);
                        }
                        
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
                UI.ShowStatus(true,selUnit.GetComponent<unit_operator>());

            }
            if (selUnit != null)
            {
                if(selected_gameob.tag == "enemy")
                {
                    print(Vector3.Distance(selUnit.transform.position, selected_gameob.transform.position));
                    
                    //TODO: #Cheking distance >>> #Cheking posibility >>> #Type of Weapon
                    //TODO: #Atack result Calculation before striking
                    //TODO: #Duoble Confirning click to strike >>> #Call function of UnitOperator to deal damage
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
