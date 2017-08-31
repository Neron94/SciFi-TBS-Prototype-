using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnUI : MonoBehaviour {
    UI_Controller UI;
  
    
    private void Awake()
    {
        UI = GameObject.Find("UI_Controller").GetComponent<UI_Controller>();
    }
    public void OnPointerEnter()
    {
        UI.MouseOnUI(true);
    }
    public void OnPointerExit()
    {
        UI.MouseOnUI(false);
    }
    
}
