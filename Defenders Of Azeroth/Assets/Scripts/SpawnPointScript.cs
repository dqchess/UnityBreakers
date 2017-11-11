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
            GameObject towerPrefab = GameObject.Find("Main Camera").GetComponent<UnitsButton>().unitPrefab;
            int cost = towerPrefab.GetComponent<EntityValue>().selfValue;

            if (GameObject.Find("map1").GetComponent<ShopScript>().DecreaseCurrency(cost)) {
                OnMouseExit();
                tower = Instantiate(towerPrefab, transform.position, Quaternion.identity) as GameObject;
                GameObject.Find("map1").GetComponent<GameScript>().AddTower(tower);

                TowerScript script = tower.GetComponentInChildren<TowerScript>();
                script.SetSpawnPoint(this);

                // TODO hardcoded tower timeout ?!
                script.SetTimeout(20f);
            }
        }
    }

    public void NotifyTowerDestroy()
    {
        GameObject.Find("map1").GetComponent<GameScript>().RemoveTower(tower);
        Destroy(tower);    
        tower = null;
    }
}
