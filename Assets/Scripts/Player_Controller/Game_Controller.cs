using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour {

    enum turn { player , ai };
    turn whoseTurn = turn.player;
    Text turnText;
    Unit_List u_list;
    Core_AI coreAI;
    UI_Controller UC;
    player_controller_operator plaContrOper;

    private void Start()
    {
        coreAI = GameObject.Find("Core_AI").GetComponent<Core_AI>();
        u_list = GameObject.Find("Battle_Controller").GetComponent<Unit_List>();
        turnText = GameObject.Find("turnText").GetComponent<Text>();
        UC = GameObject.Find("UI_Controller").GetComponent<UI_Controller>();
        plaContrOper = GameObject.Find("player_control_operator").GetComponent<player_controller_operator>();
    }

    public void EndTurn()
    {
        if(whoseTurn == turn.player)
        {

            whoseTurn = turn.ai;
            turnText.text = "Ход: Противника";
            u_list.RestoreAP(2);
            coreAI.Start_AI();
            UC.ShowStatus(false);
            plaContrOper.DiSelect();


        }
        else
        {
            turnText.text = "Ход: Игрока";
            whoseTurn = turn.player;
            u_list.RestoreAP(1);
        }
    }
}
