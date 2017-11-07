using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour {

    private float initialOpacity;
    private float highlightedOpacity = 0.5f;

    private string towerType;
    private GameObject tower = null;


    public GameObject testPrefab;


    // TODO this should be the menu script
    //public GameObject menu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnMouseEnter()
    {
        if (!tower)
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();

            Color tmp = sprite.color;
            initialOpacity = tmp.a;
            tmp.a = highlightedOpacity;
            sprite.color = tmp;
        }
    }

    private void OnMouseExit()
    {
        if (!tower)
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();

            Color tmp = sprite.color;
            tmp.a = initialOpacity;
            sprite.color = tmp;
        }
    }

    private void OnMouseOver()
    {
        if (!tower)
        {

        }
    }

    private void OnMouseDown()
    {
        // create an instance of the object saved in the menu object
        if (!tower)
        {
            // TODO get tower from the menu
            GameObject map = GameObject.Find("Main Camera");
            UnitsButton gameScriptController = map.GetComponent<UnitsButton>();
            tower = gameScriptController.unitPrefab;

            GameObject towerPrefab = tower;

            OnMouseExit();
            tower = Instantiate(towerPrefab, transform.position, Quaternion.identity) as GameObject;

            TowerScript script = tower.GetComponentInChildren<TowerScript>();
            script.SetSpawnPoint(this);

            // TODO hardcoded tower timeout ?!
            script.SetTimeout(20f);
        }
    }

    public void NotifyTowerDestroy()
    {
        Destroy(tower);
        tower = null;
    }
}
