using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chance_controller : MonoBehaviour {



    float modif = 0.20f;

    public int MidAccurateShot(int weaponAc, int CharacterAc)
    {
        print("WC" + weaponAc+"CA" + CharacterAc);
        int MAS = weaponAc + CharacterAc;
        MAS = MAS / 2;
        MAS = Mathf.CeilToInt(MAS);
        return MAS;
    } 

    public int ChanceCalculate(int weaponAc, int CharacterAc, int Distance)
    {
        //Старая формула вычисления вероятности
        /*int MAS = MidAccurateShot(weaponAc, CharacterAc);
        print("MAS"+MAS);
        int OneCellChance = 100 / MAS;
        OneCellChance = Mathf.CeilToInt(OneCellChance);
        print("OCC"+OneCellChance);

        int Difference = Mathf.Abs(Distance - MAS);
        print("Dif"+Difference);
        float BadEffect = Difference * OneCellChance;
        float ModificatorEffect = BadEffect * modif;
        BadEffect = BadEffect - ModificatorEffect;
        print(" DO Bad" + BadEffect);
        BadEffect = Mathf.Floor(BadEffect);
        print("Bad"+BadEffect);*/

        int abilityAccuracy = weaponAc + CharacterAc;
        int distancePercent = 100 / Distance;
        int dis_accuracy_difference = Distance - abilityAccuracy;
        int percentOfPenalty = dis_accuracy_difference * distancePercent;

        print(abilityAccuracy);

        if(percentOfPenalty > 100)
        {
            return 0;
        }
        else if(percentOfPenalty < 0)
        {
            return 100;
        }
        else
        {
            return 100 - percentOfPenalty;
        }
        
    }
    public bool TakeChance(int prob)
    {
        if (Random.Range(0, 100) <= prob)
        {
            return true;
        }
        else return false;
    }
	
}
