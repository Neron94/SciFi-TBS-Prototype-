﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Controller : MonoBehaviour {

    public Visual_contact visual_contact;
    public Affect_damage affect_damage;
    public Defence_test defence_test;
    public Distance_Calculator distance_calculator;
    public chance_controller chance_Controller;
    public Unit_List unit_list;


    public unit_operator attacker;
    public unit_operator defender;


    private void Start()
    {
        visual_contact = transform.GetComponentInChildren<Visual_contact>();
        affect_damage = transform.GetComponentInChildren<Affect_damage>();
        defence_test = transform.GetComponentInChildren<Defence_test>();
        distance_calculator = transform.GetComponentInChildren<Distance_Calculator>();
        unit_list = transform.GetComponentInChildren<Unit_List>();
        chance_Controller = transform.GetComponentInChildren<chance_controller>();
    }
    public int ChanceIs(unit_operator atacker, unit_operator defender)
    {
        return chance_Controller.ChanceCalculate((int)atacker.myWeapon[atacker.activeWeapon - 1].Range(), atacker.accuracy, distance_calculator.Distance(atacker, defender));
    }
    public void PrepareToStrike()
    {
        if(attacker.myWeapon[attacker.activeWeapon-1].ammo > 0)
        {
            attacker.Attack(defender.gameObject);
            if (defender.hp != 0)
            {
                int chance = chance_Controller.ChanceCalculate((int)attacker.myWeapon[attacker.activeWeapon - 1].Range(), attacker.accuracy, distance_calculator.Distance(attacker, defender));
                if (chance_Controller.TakeChance(chance))
                {
                    print("Попал");
                    Strike(attacker, defender);
                    defender.Rotation(attacker.gameObject);
                    attacker.action_point = 0;
                }
                else
                {
                    print("Промах");
                }
                //Пока будем сразу наносить урон при нажатие
            }
        }
        else
        {
            print("Кончились патроны");
        }
        


    }
    public void Strike(unit_operator attacker, unit_operator defender)
    {
        affect_damage.CalculateDamage(attacker,defender);
    }
}
