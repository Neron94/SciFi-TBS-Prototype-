﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_operator : MonoBehaviour{

    //TODO: #Unit HP system >>> #Function to deal Damage
    

    List<Square_cell_Operator> myPath = new List<Square_cell_Operator>();

    public Field_Controller.barrikade inCover = Field_Controller.barrikade.none;
    public GameObject selector;
    public GameObject target;
    public GameObject Eyes_Object;
    public Square_cell_Operator myPos;
    public Unit_List u_list;

    int action_point = 2;
    public int[] stepMinMax = new int[] {3,5};

    
    public int hp = 5;
    public List<GameObject> hp_item = new List<GameObject>();
    public float speed;
    public float rotSpeed;
    bool isMoving = false;
    int stepIndex = 0;

    List<Square_cell_Operator> NearBarrikades = new List<Square_cell_Operator>();

    Animator myAnimator;


    public List<weapon_operator> myWeapon = new List<weapon_operator>();
    public int activeWeapon = 1;

    public int accuracy = 2;



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
        u_list = GameObject.Find("Battle_Controller").GetComponent<Unit_List>();
        if(gameObject.tag != "myUnit")
        {
            u_list.enemyUnitList.Add(this);
        }
        myAnimator = gameObject.GetComponent<Animator>();
        selector = transform.Find("selector").gameObject;
        target = transform.Find("target").gameObject;
        Eyes_Object = transform.Find("Battle_Eye").gameObject;
        selector.SetActive(false);
        target.SetActive(false);


        if (myWeapon.Count > 1) //If we have more then 1 weapon
        {
            myWeapon[1].gameObject.SetActive(false); // turn off secondary weapon model
        }

        

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
            else if (transform.position == myPath[0].transform.position) // if we in Finish
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

        if(hp < 0)
        {
            hp = 0;
            print("Death");
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
    public void Detected()
    {
       
        if(target.active == true)
        {
            target.SetActive(false);
        }
        else
        {
            target.SetActive(true);
        }

    }
    public void Move(List<Square_cell_Operator> path)
    {
        inCover = Field_Controller.barrikade.none; // Выключаем баррикадный модификатор при начале движения

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
        inCover = NearBarrikades[0].Cover;
        myAnimator.SetInteger("State", 2); // АНИМАЦИЯ УКРЫТИЯ
    } //Залечь за 
    public void WeaponSwitch(int ID)
    {
       switch (ID)
        {
            case 1:
                myWeapon[0].gameObject.SetActive(true);
                myWeapon[1].gameObject.SetActive(false);
                activeWeapon = 1;
                break;
            case 2:
                myWeapon[0].gameObject.SetActive(false);
                myWeapon[1].gameObject.SetActive(true);
                activeWeapon = 2;
                break;

        }
    }

    public void HpChange(int value, int actionID)
    {
        switch(actionID)
        {
            //HP minus
            case 1:
                print("Take damage" + value);
                if(hp > 0)
                {
                    hp = hp - value;
                }
                HudHpChange(1);

                break;
           //HP plus
            case 2:
                print("Heal" + value);
                break;
        }
    }
    //Заплатка*** выисляю по макс ХП 5
    public void HudHpChange( int actionID)
    {
        switch (actionID)
        {
            //HP minus
            case 1:
                for (int  i = 5 - hp;  i == 0; i --)
                {
                    hp_item[i - 1].SetActive(false);
                }
                break;
            //HP plus
            case 2:
                /*
                for (int i = 5 - hp; i == 0; i--)
                {
                    hp_item[i - 1].SetActive(false);
                }*/
                print("Пока не реализованн ХИЛЛ");
                break;
        }
            
            
        
    }

}