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
        int MAS = MidAccurateShot(weaponAc, CharacterAc);
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
        print("Bad"+BadEffect);
    

        return 100 - (int)BadEffect;
    }
	
}
