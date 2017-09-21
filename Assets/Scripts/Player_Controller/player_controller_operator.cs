using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller_operator : MonoBehaviour {

    Square_cell_Operator chosenSquare;
    Coordinate_System_Operator CSO;
    Color colBarri = new Color(255,0,0,255);
    GameObject selUnit;
    UI_Controller UI;
    ShowAvailableMoves SquareShow;
    squarePainter SquarePainter;
    Battle_Controller Battle_controller;

    private void Start()
    {
        CSO = GameObject.Find("Coordinate_System_Manager").GetComponent<Coordinate_System_Operator>();
        UI = GameObject.Find("UI_Controller").GetComponent<UI_Controller>();
        SquareShow = GameObject.Find("ShowAvailableMoves").GetComponent<ShowAvailableMoves>();
        SquarePainter = GameObject.Find("Level_Controller").GetComponent<squarePainter>();
        Battle_controller = GameObject.Find("Battle_Controller").GetComponent<Battle_Controller>();
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

                if (selUnit && selUnit.GetComponent<unit_operator>().action_point > 0)
                {
                    if (selected_gameob.tag == "square")
                    {
                        //If almost no unit in this CUBE
                        if(!selected_gameob.GetComponent<Square_cell_Operator>().haveUnitOn)
                        {
                            selUnit.GetComponent<unit_operator>().Move(CSO.GetPath(selUnit.GetComponent<unit_operator>().myPos, selected_gameob.GetComponent<Square_cell_Operator>()));
                            SquarePainter.PaintSquares(SquareShow.ShowingSquaresMin, 4);
                            SquarePainter.PaintSquares(SquareShow.ShowingSquaresMax, 4);

                            Battle_controller.visual_contact.ClearContact(); // Зачистка Листа с вражескими юнитами

                            if(SquareShow.WhichListIsTargetSquare(SquareShow.ShowingSquaresMin, selected_gameob.GetComponent<Square_cell_Operator>()))
                            {
                                selUnit.GetComponent<unit_operator>().action_point--;
                            }
                            else if(SquareShow.WhichListIsTargetSquare(SquareShow.ShowingSquaresMax, selected_gameob.GetComponent<Square_cell_Operator>()))
                            {
                                selUnit.GetComponent<unit_operator>().action_point = 0;
                            }
                            selUnit = null;
                            selected_gameob = null;
                            UI.ShowStatus(false);

                        }
                        
                    }

                    
                }
                else
                {
                    if(selUnit != null) selUnit.GetComponent<unit_operator>().Select();

                }

            }
            //Unit Chosing
            if(selected_gameob != null && selected_gameob.tag == "myUnit")
            {
                selUnit = null;
                selected_gameob.GetComponent<unit_operator>().Select();
                selUnit = selected_gameob;
                UI.ShowStatus(true,selUnit.GetComponent<unit_operator>());

                if (selected_gameob.GetComponent<unit_operator>().action_point > 0)
                {
                    SquarePainter.PaintSquares(SquareShow.Show("min", selUnit.GetComponent<unit_operator>().stepMinMax[0], selUnit.GetComponent<unit_operator>().stepMinMax[1], selUnit.GetComponent<unit_operator>().myPos), 1);
                    SquarePainter.PaintSquares(SquareShow.Show("max", selUnit.GetComponent<unit_operator>().stepMinMax[0], selUnit.GetComponent<unit_operator>().stepMinMax[1], selUnit.GetComponent<unit_operator>().myPos), 3);
                    Battle_controller.visual_contact.EnemyContact(selUnit.GetComponent<unit_operator>());

                }


            }
            if (selUnit != null)
            {
                //Выбор противника
                if(selected_gameob.tag == "enemy") 
                {

                    if(Battle_controller.visual_contact.Visual_Contact(selUnit.GetComponent<unit_operator>(), selected_gameob.GetComponent<unit_operator>()))
                    {
                        //Visual contact
                        print("Visual contact");
                        Battle_controller.PrepareToStrike(selUnit.GetComponent<unit_operator>(), selected_gameob.GetComponent<unit_operator>());                
                    }
                   


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
                selected_gameob.GetComponent<Square_cell_Operator>().Cover = Field_Controller.barrikade.half;
            }

            
        }
    }
}
