using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour {

    public unitStatusUI_operator unitStatus;
    public attack_ui_Controller attack_ui;
    public bool attack_status = false;
    public bool statusMenuState = false;
    public bool onUI = false;

    private void Awake()
    {
        unitStatus = GetComponentInChildren<unitStatusUI_operator>();
        unitStatus.gameObject.SetActive(statusMenuState);
        attack_ui = GetComponentInChildren<attack_ui_Controller>();
        attack_ui.gameObject.SetActive(attack_status);
    }

    public void ShowStatus(bool show, unit_operator unit)
    {
        
        statusMenuState = show;
        unitStatus.gameObject.SetActive(statusMenuState);
        unitStatus.StatusShow(unit);
    }
    public void ShowStatus(bool show)
    {

        statusMenuState = show;
        unitStatus.gameObject.SetActive(statusMenuState);
        
    }
    public void MouseOnUI(bool isOnUI)
    {
        onUI = isOnUI;
    }

    public void ShowAtackStatus(bool isShow, string _name, int _myHp, int _prob)
    {
        attack_status = isShow;
        attack_ui.gameObject.SetActive(attack_status);
        attack_ui.ShowAttack_menu(_name,_myHp,_prob);

    }
    public void ShowAtackStatus(bool isShow)
    {
        attack_status = isShow;
        attack_ui.gameObject.SetActive(attack_status);
     }
}
