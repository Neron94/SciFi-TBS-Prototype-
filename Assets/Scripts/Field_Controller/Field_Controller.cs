using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field_Controller : MonoBehaviour {

    public enum barrikade { none, half, full };
    // Класс в целом пока используется только для содержания полного списка поля (для инициализации окружающих кубов точки)
    List<Square_cell_Operator> Field_List = new List<Square_cell_Operator>();

    //Свойство получения списка
    public List<Square_cell_Operator> _Field_List()
    {
        return Field_List;
    }
    //Свойство Добавления в список
    public void AddSquareToList(Square_cell_Operator add)
    {
        Field_List.Add(add);
    }
}
