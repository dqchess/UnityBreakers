using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour { 

    public GameObject towerPrefab;

    public void SetTowerPrefab(GameObject tower)
    {
        this.towerPrefab = tower;
    }

    public GameObject defenderPrefab;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;

    public Transform enemySpawnPointUp;
    public Transform enemySpawnPointDown;
    public Transform enemyTarget;

    private GameObject[] enemyPrefabs;

    private List<GameObject> spawnedDefenders = new List<GameObject>();
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    float elapsedTime = 0f;
    float targetTime = 5f;

	// Use this for initialization
	void Start () {
        enemyPrefabs = new GameObject[3];
        enemyPrefabs[0] = enemy1Prefab;
        enemyPrefabs[1] = enemy2Prefab;
        enemyPrefabs[2] = enemy3Prefab;
    }
	
	// Update is called once per frame
	void Update () {
        // wave system
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= targetTime)
            {
                elapsedTime = 0;

                int numEnemiesToSpawn = 1 + (int)(Random.value * 100) % 2;
                bool spawnUp = ( (int)(Random.value * 100) % 2) == 1;
                int enemyToSpawn = ((int)(Random.value * 100) % 3);

                Transform spawnLocation = (spawnUp) ? enemySpawnPointUp : enemySpawnPointDown;

                for (int i = 0; i < numEnemiesToSpawn; i++)
                {
                    GameObject enemy = Instantiate(enemyPrefabs[enemyToSpawn], spawnLocation.position - new Vector3(30 * i, 0, 0), enemy1Prefab.GetComponent<Transform>().rotation);
                    enemy.GetComponent<AIPath>().target = enemyTarget;
                    enemy.GetComponent<AIPath>().SearchPath();

                    spawnedEnemies.Add(enemy);
                }
            }
        }
    }

    public void NotifyEnemyDestroy(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);

        foreach (GameObject e in spawnedDefenders)
        {
            e.GetComponentInChildren<Defender_Controller>().NotifyEnemyDead(enemy);
        }

        Destroy(enemy);

        // Add currency and score
        ShopScript shop = GetComponent<ShopScript>();

        shop.AddScore(enemy.GetComponent<EnemyController>().GetMaxHealth());
        shop.AddCurrency(enemy.GetComponent<EnemyController>().GetMaxHealth());
    }

    public GameObject GetNearestEnemy(Vector3 position, float maxRange)
    {
        float distance = maxRange;
        GameObject closest = null;

        foreach (GameObject enemy in spawnedEnemies) {
            float temp = Vector3.Distance(enemy.GetComponent<Transform>().position, position);
            if (temp < distance)
            {
                distance = temp;
                closest = enemy;
            }
        }

        return closest;
    }

    public void SpawnDefender()
    {
        int cost = defenderPrefab.GetComponent<EntityValue>().selfValue;
        if (GetComponent<ShopScript>().DecreaseCurrency(cost))
        {
            Vector3 location = GameObject.Find("mainBase").GetComponent<Transform>().position - new Vector3(120, 0, 0);
            GameObject defender = Instantiate(defenderPrefab, location, defenderPrefab.GetComponent<Transform>().rotation);
            spawnedDefenders.Add(defender);
        }
    }
}
