using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field_Controller : MonoBehaviour {

    List<Square_cell_Operator> Field_List = new List<Square_cell_Operator>();

    public List<Square_cell_Operator> _Field_List()
    {
        return Field_List;
    }

    public void AddSquareToList(Square_cell_Operator add)
    {
        Field_List.Add(add);
    }
}
