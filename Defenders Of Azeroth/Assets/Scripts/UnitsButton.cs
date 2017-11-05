using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsButton : MonoBehaviour {

    [SerializeField]
    private GameObject unitPrefab;

    public GameObject UnitPrefab
    {
        get
        {
            return unitPrefab;
        }

    }
       
    public void Log(string ta)
    {
        Debug.Log(this.unitPrefab);
    }

}
