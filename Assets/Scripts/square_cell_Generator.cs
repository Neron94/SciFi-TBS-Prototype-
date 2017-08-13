using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class square_cell_Generator : MonoBehaviour {
     
    GameObject square_cell;                  // Сам обьект Единица Сетки
    GameObject FieldObject;                  // Обьект на котором будет спавнится сетка
    Vector3 WhereSpawningStarts;           // Точка начала спавна сетки
    public int[] SquareGridSize = new int[2];
    Field_Controller _Field_Controller;
    


    private void Start()
    {
        _Field_Controller = GetComponentInParent<Field_Controller>();
        FieldObject = GameObject.Find("Field");
        square_cell = GameObject.Find("square_cell");
        WhereSpawningStarts = square_cell.transform.position;
        GenerateSquareField(SquareGridSize[0],SquareGridSize[1]);
        
    }

    bool SearchPositionToSpawn() // Проверка Есть ли Начальная позиция для спавна сетки
    {
        if(WhereSpawningStarts != null) // Если позиции нет
        {
            // Ищем точку для начала спавна
            return true;
        }
        else
        {
            return true;
        }

    }
    void GenerateSquareField(int x, int y)
    {
        if (SearchPositionToSpawn()) //Если имеем обьект для начала спавна то Начинаем
        {
           

            for (int x_num = 1; x_num <= x; x_num++)
            {
                
                for (int y_num = 1; y_num <= y; y_num++)
                {
                    GameObject square_cell_new = Instantiate(square_cell, WhereSpawningStarts, transform.rotation);
                    square_cell_new.transform.SetParent(FieldObject.transform);
                    square_cell_new.GetComponent<Square_cell_Operator>().SetCoordinate(x_num,y_num);
                    square_cell_new.transform.position = new Vector3(square_cell.transform.position.x + y_num - 1, square_cell.transform.position.y, square_cell.transform.position.z);
                    _Field_Controller._Field_List().Add(square_cell_new.GetComponent<Square_cell_Operator>());
                }
                square_cell.transform.position = new Vector3(square_cell.transform.position.x, square_cell.transform.position.y, square_cell.transform.position.z - 1);
            }
            
        }
        else
        {
            print("Не имеем позиции для начала спавна Сетки");
        }
    }
}
