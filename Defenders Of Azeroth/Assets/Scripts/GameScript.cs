using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {
    public GameObject towerPrefab;

    public UnitsButton ClickedBtn { get; private set; }


    public GameObject enemy1Prefab;
    public Transform enemySpawnPointUp;
    public Transform enemySpawnPointDown;
    public Transform enemyTarget;

    float elapsedTime = 0f;
    float targetTime = 5f;

    // wave system relate
    
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

        // wave system
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= targetTime)
            {
                elapsedTime = 0;

                int numEnemiesToSpawn = 1 + (int)(Random.value * 100) % 2;
                bool spawnUp = ( (int)(Random.value * 100) % 2) == 1;

                Transform spawnLocation = (spawnUp) ? enemySpawnPointUp : enemySpawnPointDown;

                for (int i = 0; i < numEnemiesToSpawn; i++)
                {
                    GameObject enemy = Instantiate(enemy1Prefab, spawnLocation.position - new Vector3(30 * i, 0, 0), enemy1Prefab.GetComponent<Transform>().rotation);
                    enemy.GetComponent<AIPath>().target = enemyTarget;
                    enemy.GetComponent<AIPath>().SearchPath();
                }
            }
        }
    }



}
