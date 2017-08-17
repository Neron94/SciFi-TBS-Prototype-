using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_operator : MonoBehaviour {

    List<Square_cell_Operator> myPath = new List<Square_cell_Operator>();
    public GameObject selector;
    public Square_cell_Operator myPos;

    
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "square")
        {
            myPos = collider.gameObject.GetComponent<Square_cell_Operator>();
        }
    }

    private void Start()
    {        
        selector = GameObject.Find("selector");
        selector.SetActive(false);
    }


    private void Update()
    {
        
    }

    public void Select()
    {
        if(selector.active)
        {
            selector.SetActive(false);
        }
        else
        {
            selector.SetActive(true);
        }
    }

    public void Move(List<Square_cell_Operator> path)
    {

    }
}
