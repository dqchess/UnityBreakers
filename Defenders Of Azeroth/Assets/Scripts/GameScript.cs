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
    public GameObject catapultPrefab;

    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public GameObject bossPrefab;

    private List<GameObject> towerList = new List<GameObject>();

    public Transform enemySpawnPointUp;
    public Transform enemySpawnPointDown;
    public Transform enemyTarget;

    private GameObject[] enemyPrefabs;

    private List<GameObject> spawnedDefenders = new List<GameObject>();
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    float elapsedTime = 15f;
    float targetTime = 10f;
    float bossSpawnTime = 40f;
    float elapsedBossTime = 0f;
    public int nrCatapults = 0;

	// Use this for initialization
	void Start () {
        enemyPrefabs = new GameObject[3];
        enemyPrefabs[0] = enemy1Prefab;
        enemyPrefabs[1] = enemy2Prefab;
        enemyPrefabs[2] = enemy3Prefab;
        AudioListener.volume = 0.1f;
    }

    float enemyHPIncrease = 0.0f;
    float bossHPIncrease = 0.0f;

	// Update is called once per frame
	void Update () {
        // wave system
        {
            elapsedTime += Time.deltaTime;
            elapsedBossTime += Time.deltaTime;
            
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
                    enemy.GetComponentInChildren<EnemyController>().enemyMaxHealth += enemyHPIncrease;
                    enemy.GetComponentInChildren<EnemyController>().enemyCurrentHealth = enemy.GetComponentInChildren<EnemyController>().enemyMaxHealth;
                    enemy.GetComponent<AIPath>().target = enemyTarget;
                    enemy.GetComponent<AIPath>().SearchPath();

                    spawnedEnemies.Add(enemy);

                    enemyHPIncrease += 5;
                }
            }

            if (elapsedBossTime >= bossSpawnTime)
            {
                elapsedBossTime = 0f;

                bool spawnUp = ((int)(Random.value * 100) % 2) == 1;
                Transform spawnLocation = (spawnUp) ? enemySpawnPointUp : enemySpawnPointDown;
                GameObject enemy = Instantiate(bossPrefab, spawnLocation.position, enemy1Prefab.GetComponent<Transform>().rotation);
                enemy.GetComponentInChildren<EnemyController>().enemyMaxHealth += bossHPIncrease;
                enemy.GetComponentInChildren<EnemyController>().enemyCurrentHealth = enemy.GetComponentInChildren<EnemyController>().enemyMaxHealth;
                enemy.GetComponent<AIPath>().target = enemyTarget;
                enemy.GetComponent<AIPath>().SearchPath();

                spawnedEnemies.Add(enemy);

                bossHPIncrease += 10;
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
        // Add currency and score
        ShopScript shop = GetComponent<ShopScript>();

        shop.AddScore(enemy.GetComponentInChildren<EnemyController>().GetMaxHealth());
        shop.AddCurrency(enemy.GetComponentInChildren<EnemyController>().GetMaxHealth());
        PlayerPrefs.SetFloat("lastScore",shop.getScore());

        Destroy(enemy);
    }

    public void NotifyDefenderDestroy(GameObject defender)
    {
        spawnedDefenders.Remove(defender);
        Destroy(defender);
    }

    public GameObject GetNearestEnemy(Vector3 position, float maxRange, float minRange = 0)
    {
        float distance = maxRange;
        GameObject closest = null;

        foreach (GameObject enemy in spawnedEnemies) {
            float temp = Vector3.Distance(enemy.GetComponent<Transform>().position, position);
            if (temp <= distance && temp >= minRange)
            {
                distance = temp;
                closest = enemy;
            }
        }

        return closest;
    }

    public GameObject GetNearestDefender(Vector3 position, float maxRange, float minRange = 0)
    {
        float distance = maxRange;
        GameObject closest = null;

        foreach (GameObject defender in spawnedDefenders)
        {
            float temp = Vector3.Distance(defender.GetComponent<Transform>().position, position);
            if (temp < distance && temp >= minRange)
            {
                distance = temp;
                closest = defender;
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
            defender.GetComponent<AIPath>().speed = 65;
            spawnedDefenders.Add(defender);
        }
    }

    public void SpawnCatapult()
    {
        if (nrCatapults == 2)
            return;

        if (!GameObject.Find("map1").GetComponent<ShopScript>().DecreaseCurrency(catapultPrefab.GetComponent<EntityValue>().selfValue))
            return;

        nrCatapults++;
        if (nrCatapults == 1);
        {
            Vector3 location = GameObject.Find("mainBase").GetComponent<Transform>().position - new Vector3(-35, 120,0);
            //GameObject catapult = Instantiate(catapultPrefab, location, catapultPrefab.GetComponent<Transform>().rotation);
            GameObject catapult = Instantiate(catapultPrefab, location, Quaternion.identity);

            //spawnedDefenders.Add(catapult);
        }
        if (nrCatapults == 2)
        {
            Vector3 location = GameObject.Find("mainBase").GetComponent<Transform>().position - new Vector3(-35, -120, 0);
            GameObject catapult = Instantiate(catapultPrefab, location, Quaternion.identity);
            //spawnedDefenders.Add(catapult);
        }    
    }
    public void AddTower(GameObject tower)
    {
        towerList.Add(tower);
    }

    public void RemoveTower(GameObject tower)
    {
        towerList.Remove(tower);
    }

    public void Upgrade_Towers_Damage ()
    {
        if (GameObject.Find("map1").GetComponent<ShopScript>().DecreaseCurrency(1000))
        {
            foreach (var tower in towerList)
            {
                tower.GetComponentInChildren<TowerScript>().bulletDamage = tower.GetComponentInChildren<TowerScript>().bulletDamage * 1.5f;
            }
        }
    }
    public void Upgrade_Towers_Range()
    {
        if (GameObject.Find("map1").GetComponent<ShopScript>().DecreaseCurrency(1200))
        {
            foreach (var tower in towerList)
            {
                tower.GetComponentInChildren<TowerScript>().bulletRange = tower.GetComponentInChildren<TowerScript>().bulletRange * 1.5f;

            }
        }
    }

    public void Upgrade_Defender_SpeedDamage()
    {
        if (GameObject.Find("map1").GetComponent<ShopScript>().DecreaseCurrency(2000))
        {
            foreach (var defender in spawnedDefenders)
            {
                defender.GetComponent<AIPath>().speed = defender.GetComponent<AIPath>().speed * 1.5f;
                defender.GetComponentInChildren<Defender_Controller>().damageAmount = defender.GetComponentInChildren<Defender_Controller>().damageAmount * 1.5f;
            }
        }
    }

    public void Upgrade_Enemy_ReduceSpeedDamage()
    {
        if (GameObject.Find("map1").GetComponent<ShopScript>().DecreaseCurrency(1500))
        {
            foreach (var enemy in spawnedEnemies)
            {
                enemy.GetComponent<AIPath>().speed -= enemy.GetComponent<AIPath>().speed * 0.2f;
                enemy.GetComponentInChildren<EnemyController>().enemyHitDamage -= enemy.GetComponentInChildren<EnemyController>().enemyHitDamage * 0.2f;
            }
        }
    }

    //public void Upgrade_Enemy_SuddenDeath()
    //{
    //    var b = spawnedEnemies;

    //    foreach (var enemy in spawnedEnemies)
    //    {
    //        enemy.GetComponentInChildren<EnemyController>().InflictDamage(200);
    //    }
    //    var a = spawnedEnemies;

    //}
}
