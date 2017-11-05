using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsButton : MonoBehaviour {

    
    public GameObject unitPrefab;

    private void Update()
    {
       
    }

    public void SetTowerPrefab(GameObject tower)
    {
        this.unitPrefab = tower;
    }

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
