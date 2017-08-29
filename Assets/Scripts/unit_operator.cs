using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_operator : MonoBehaviour{
    //TODO #Unit ActionPoint >>> #Show available paths method
    //TODO: #Unit HP system >>> #Function to deal Damage
    //TODO: #Weapon System (Type, parameters, distance)
    

    List<Square_cell_Operator> myPath = new List<Square_cell_Operator>();
    public GameObject selector;
    public Square_cell_Operator myPos;

    public float speed;
    public float rotSpeed;
    bool isMoving = false;
    int stepIndex = 0;
    List<Square_cell_Operator> NearBarrikades = new List<Square_cell_Operator>();
    Animator myAnimator;



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "square")
        {
            other.GetComponent<Square_cell_Operator>().haveUnitOn = false;
        }
    }
    private void OnTriggerStay(Collider collider)
    {

        if(collider.gameObject.tag == "square")
        {
                myPos = collider.gameObject.GetComponent<Square_cell_Operator>();
                myPos.haveUnitOn = true;  
        }
    }



    private void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
        selector = GameObject.Find("selector");
        selector.SetActive(false);
        
    }


    private void Update()
    {
        // Передвижение персонажа скрипт
        if(isMoving)
        {
            

            if (transform.position != myPath[0].transform.position) // Пока не достигли КОНЦА пути
            {
                
                Quaternion toRotation = Quaternion.LookRotation(myPath[stepIndex].transform.position - transform.position, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * rotSpeed);

               transform.position = Vector3.MoveTowards(transform.position, myPath[stepIndex].transform.position, speed * Time.deltaTime);

                if (transform.position == myPath[stepIndex].transform.position)
                {

                    if (myPath.Count > stepIndex)
                    {
                        stepIndex--;
                    }

                }
            }
            else if (transform.position == myPath[0].transform.position)
            {
                isMoving = false;
                myPath.Clear();
                Select();
                stepIndex = 0;
                myAnimator.SetInteger("State", 0);
                if(BarrikadaTest(myPos))
                {
                    TakeCover();
                    NearBarrikades.Clear();
                }
            }

               
            
            
        }
        
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
        myPath = path;
        if(myPath.Count != 0)
        {
            isMoving = true;
            stepIndex = myPath.Count - 2;
            myAnimator.SetInteger("State", 1);
        }
    }

    bool BarrikadaTest(Square_cell_Operator point)
    {
        bool have = false;
        foreach(Square_cell_Operator square in point.Around_Squares)
        {
            int currentID = myPos.GetCoordinates()[0] + myPos.GetCoordinates()[1];
            int checkID = square.GetCoordinates()[0] + square.GetCoordinates()[1];
            if(square.barrier)
            {
                //Проверка баррикада по диогонали?
                if(Mathf.Abs(currentID - checkID) == 2 || Mathf.Abs(currentID - checkID) == 0)
                {
                    //Сюда попадают если находимся по диогонали
                }
                else
                {
                    NearBarrikades.Add(square);
                    have = true;
                }
                
            }
        }
        return have;
    } // Проверка нет ли рядом баррикад


    void TakeCover()
    {
        Quaternion toRotation;
        toRotation = Quaternion.LookRotation(NearBarrikades[0].transform.position - transform.position, Vector3.up);
        transform.rotation = toRotation;
        myAnimator.SetInteger("State", 2); // АНИМАЦИЯ УКРЫТИЯ
    } //Залечь за 

}
