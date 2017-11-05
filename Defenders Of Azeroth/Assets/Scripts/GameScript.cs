using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {
    public GameObject towerPrefab;

    public UnitsButton ClickedBtn { get; private set; } 


	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        //if (input.getmousebuttondown(0))
        //{
        //    debug.log("pressed left click.");
        //    vector3 offset = new vector3(120, -50, 0);
        //    vector3 mousepos = camera.main.screentoworldpoint(input.mouseposition);
        //    vector3 towerpos = new vector3(mousepos.x, mousepos.y, 0);
        //    gameobject tower = (gameobject)instantiate(towerprefab, towerpos, quaternion.identity);
        //}

    }


}
