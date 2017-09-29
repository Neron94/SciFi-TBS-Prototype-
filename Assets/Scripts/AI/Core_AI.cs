using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_AI : MonoBehaviour {

    public List<ai_squad> all_squad = new List<ai_squad>();
    public Field_Controller FC;
    public Battle_Controller BC;
    private void Start()
    {
        FC = GameObject.Find("Field_Controller").GetComponent<Field_Controller>();
        BC = GameObject.Find("Battle_Controller").GetComponent<Battle_Controller>();
    }


    public void Start_AI()
    {
        foreach(ai_squad squads in all_squad)
        {
            squads.Order();
        }
    }

}
