using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller_operator : MonoBehaviour {

    Square_cell_Operator chosenSquare;
    Coordinate_System_Operator CSO;

    private void Start()
    {
        CSO = GameObject.Find("Coordinate_System_Manager").GetComponent<Coordinate_System_Operator>();
    }



    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(mouseRay, out hit, Mathf.Infinity);
            GameObject selected_gameob = hit.collider.gameObject;

            if(selected_gameob.tag == "square")
            {

                if (chosenSquare)
                {
                    CSO.Find_path(chosenSquare.GetComponent<Square_cell_Operator>(), selected_gameob.GetComponent<Square_cell_Operator>());
                    chosenSquare = null;
                    print("Запуск системы поиска");
                }
                else
                {
                    print("Клик по Кубу");
                    chosenSquare = selected_gameob.GetComponent<Square_cell_Operator>();
                }
            }
        }
    }
}
