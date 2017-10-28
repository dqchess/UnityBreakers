using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {
    public GameObject towerPrefab;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left click.");
            //Vector3 offset = new Vector3(120, -50, 0);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 towerPos = new Vector3(mousePos.x, mousePos.y, 0);
            GameObject tower = (GameObject)Instantiate(towerPrefab, towerPos, Quaternion.identity);
        }
    }
}
