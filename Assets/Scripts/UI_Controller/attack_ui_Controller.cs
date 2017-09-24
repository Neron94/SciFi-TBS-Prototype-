using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attack_ui_Controller : MonoBehaviour {

    
    Image hp;
    Text enemy_name;
    Text prob;


    private void Awake()
    {
        
        hp = transform.Find("Panel").transform.Find("hp").GetComponent<Image>();
        enemy_name = transform.Find("Panel").transform.Find("name").GetComponent<Text>();
        prob = transform.Find("Panel").transform.Find("prob").GetComponent<Text>();
    }

    public void ShowAttack_menu(string name, int myHp, int _prob)
    {
        hp.fillAmount = 2 * myHp * 0.1f;
        enemy_name.text = name;
        prob.text = ""+_prob;
    }
}
