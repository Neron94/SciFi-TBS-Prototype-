using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon_operator : MonoBehaviour {

    unit_operator myUnit;
    public Text ammoStatusText;
    public Button reloadBut;


    private void Awake()
    {
        myUnit = GetComponentInParent<unit_operator>();
        myUnit.myWeapon.Add(this);
        if (reloadBut != null)
        {
            reloadBut.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
         AmmoStatus(0);
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
    public int ammo;
    [SerializeField]
    int MaxAmmo;
    [SerializeField]
    int apRequared;
    


    public float Range()
    {
        return range;
    }

    public void Reload()
    {
        if(myUnit.action_point > 0)
        {
            myUnit.action_point--;
            ammo = MaxAmmo;
            //Прикрутим подбор патронов из инвентаря
            reloadBut.gameObject.SetActive(false);
            ammoStatusText.text = ammo + "/" + MaxAmmo;
        }
        
    }

    public void AmmoStatus(int ammoShot)
    {
        ammo = ammo - ammoShot;
        ammoStatusText.text = ammo + "/" + MaxAmmo;
        if(ammo == 0)
        {
           reloadBut.gameObject.SetActive(true);
        }
        else
        {
            reloadBut.gameObject.SetActive(false);
        }
    }
    

}
