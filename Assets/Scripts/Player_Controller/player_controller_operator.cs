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
                if (UI.attack_status) UI.ShowAtackStatus(false); // Выключение Меню атаки если проиошел клик

                if (selUnit && selUnit.GetComponent<unit_operator>().action_point > 0)
                {
                    if (selected_gameob.tag == "square")
                    {
                        //If almost no unit in this CUBE
                        if(!selected_gameob.GetComponent<Square_cell_Operator>().haveUnitOn)
                        {
                            

                            SquarePainter.PaintSquares(SquareShow.ShowingSquaresMin, 4);
                            SquarePainter.PaintSquares(SquareShow.ShowingSquaresMax, 4);

                            Battle_controller.visual_contact.ClearContact(); // Зачистка Листа с вражескими юнитами

                            if(SquareShow.WhichListIsTargetSquare(SquareShow.ShowingSquaresMin, selected_gameob.GetComponent<Square_cell_Operator>()))
                            {
                                selUnit.GetComponent<unit_operator>().action_point--;
                                selUnit.GetComponent<unit_operator>().Move(CSO.GetPath(selUnit.GetComponent<unit_operator>().myPos, selected_gameob.GetComponent<Square_cell_Operator>()));
                            }
                            else if(SquareShow.WhichListIsTargetSquare(SquareShow.ShowingSquaresMax, selected_gameob.GetComponent<Square_cell_Operator>()))
                            {
                                selUnit.GetComponent<unit_operator>().action_point = 0;
                                selUnit.GetComponent<unit_operator>().Move(CSO.GetPath(selUnit.GetComponent<unit_operator>().myPos, selected_gameob.GetComponent<Square_cell_Operator>()));
                            }
                            selUnit.GetComponent<unit_operator>().Select(false);
                            selUnit = null;
                            selected_gameob = null;
                            UI.ShowStatus(false);

                        }
                        
                    }

                    
                }
                else
                {
                    if(selUnit != null) selUnit.GetComponent<unit_operator>().Select(false);
                    if (UI.attack_status) UI.ShowAtackStatus(false); // Выключение Меню атаки если проиошел клик
                }

            }
            //Unit Chosing
            if(selected_gameob != null && selected_gameob.tag == "myUnit")
            {
                if (UI.attack_status) UI.ShowAtackStatus(false); // Выключение Меню атаки если проиошел клик
                selUnit = null;
                selected_gameob.GetComponent<unit_operator>().Select(true);
                selUnit = selected_gameob;
                UI.ShowStatus(true,selUnit.GetComponent<unit_operator>());

                if (selected_gameob.GetComponent<unit_operator>().action_point > 0)
                {
                    SquarePainter.PaintSquares(SquareShow.Show("min", selUnit.GetComponent<unit_operator>().stepMinMax[0], selUnit.GetComponent<unit_operator>().stepMinMax[1], selUnit.GetComponent<unit_operator>().myPos), 1);
                    if(selected_gameob.GetComponent<unit_operator>().action_point > 1)
                    {
                        SquarePainter.PaintSquares(SquareShow.Show("max", selUnit.GetComponent<unit_operator>().stepMinMax[0], selUnit.GetComponent<unit_operator>().stepMinMax[1], selUnit.GetComponent<unit_operator>().myPos), 3);

                    }
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
                        if (selUnit.GetComponent<unit_operator>().action_point >= 1)
                        {
                            //Противник в зоне видимости
                            Battle_controller.attacker = selUnit.GetComponent<unit_operator>();
                            Battle_controller.defender = selected_gameob.GetComponent<unit_operator>();
                            UI.ShowAtackStatus(true, selected_gameob.name,selected_gameob.GetComponent<unit_operator>().hp,Battle_controller.ChanceIs(selUnit.GetComponent<unit_operator>(), selected_gameob.GetComponent<unit_operator>()));
                            //Battle_controller.PrepareToStrike(selUnit.GetComponent<unit_operator>(), selected_gameob.GetComponent<unit_operator>());
                            selUnit.GetComponent<unit_operator>().action_point = 0;
                            SquarePainter.PaintSquares(SquareShow.ShowingSquaresMin, 4);
                            SquarePainter.PaintSquares(SquareShow.ShowingSquaresMax, 4);
                            
                        }
                        else print("Недостаточно Очков Действия");
                    }
                   


                    
                    
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

    public void DiSelect()
    {
        if(selUnit != null)
        {
            selUnit.GetComponent<unit_operator>().Select(false);
        }
    }
}
