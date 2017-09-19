using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance_Calculator : MonoBehaviour {

	public int Distance (unit_operator unit, unit_operator enemy)
    {
        return Mathf.Abs((int)Vector3.Distance(unit.transform.position, enemy.transform.position));
        
    }
}
