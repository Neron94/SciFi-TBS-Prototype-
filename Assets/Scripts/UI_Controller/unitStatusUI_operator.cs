using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unitStatusUI_operator : MonoBehaviour {

    Image unitImage;
    Button weapon1;
    Button weapon2;
    unit_operator myUnit;

    private void Awake()
    {
        unitImage = transform.Find("unitImage").GetComponent<Image>();
        weapon1 = transform.Find("weaponButton1").GetComponent<Button>();
        weapon2 = transform.Find("weaponButton2").GetComponent<Button>();
        weapon1.interactable = false;
    }


    public void StatusShow(unit_operator unit)
    {
        myUnit = unit;
    }

    public void WeaponButtonPress(int id)
    {
        myUnit.WeaponSwitch(id);
        switch (id)
        {
            case 1:
                weapon1.interactable = false;
                weapon2.interactable = true;
                break;
            case 2:
                weapon2.interactable = false;
                weapon1.interactable = true;
                break;
        }
    }
}
