using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_operator : MonoBehaviour {


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
