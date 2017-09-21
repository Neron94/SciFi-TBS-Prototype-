using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affect_damage : MonoBehaviour {

	
    public void CalculateDamage(unit_operator attacker, unit_operator defender)
    {
        int damage = attacker.attackPower;
        int defence = defender.defence;

        defender.HpChange(damage - defence, 1);
    }
    

}
