using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour {

    public unitStatusUI_operator unitStatus;
    public bool statusMenuState = false;
    public bool onUI = false;

    private void Awake()
    {
        unitStatus = GetComponentInChildren<unitStatusUI_operator>();
        unitStatus.gameObject.SetActive(statusMenuState);
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
}
