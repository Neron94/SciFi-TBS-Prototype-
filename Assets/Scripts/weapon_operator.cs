using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_operator : MonoBehaviour {

    unit_operator myUnit;

    private void Start()
    {
        myUnit = GetComponentInParent<unit_operator>();
        myUnit.myWeapon.Add(this);
    }


    enum weaponClass { pistol, rifle, coldWeapon };

    [SerializeField]
    string title;
    [SerializeField]
    weaponClass classWeapon;
    [SerializeField]
    float range;
    [SerializeField]
    float damage;
    [SerializeField]
    float criticalChance;
    [SerializeField]
    int ammo;
    [SerializeField]
    int apRequared;
    
    

}
