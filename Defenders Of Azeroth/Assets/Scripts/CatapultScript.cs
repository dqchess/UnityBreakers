using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatapultScript : MonoBehaviour
{
    public Rigidbody2D Catapult_Bullet;
    public GameObject healthBar;

    private SpawnPointScript spawnPoint = null;

    private float timeLeft = 0f;
    private float timeout = 0f;
    private bool timerRunning = false;
    private float bulletDamage = 350f;
    private float bulletRange = 30000f;
    private float fireFrequencySeconds = 3f;
    private float minRange = 250f;


    private float fireTimerElapsed = 0;

    // Use this for initialization
    void Start()
    {
        //healthBar.transform.SetParent(transform, false);
        SetTimeout(200f);
    }

    // Update is called once per frame
    void Update()
    {

        // TODO should fire once every 1 second -> the frequency could be increased as an update
        fireTimerElapsed += Time.deltaTime;

        if (fireTimerElapsed >= fireFrequencySeconds)
        {
            fireTimerElapsed = 0f;          
            GameObject nearsestEnemy = GameObject.Find("map1").GetComponent<GameScript>().GetNearestEnemy(transform.position, bulletRange, minRange);
            if (nearsestEnemy)
                {
                    Rigidbody2D bullet = Instantiate(Catapult_Bullet, transform.position, Quaternion.identity) as Rigidbody2D;
                    BulletScript bulletScript = bullet.gameObject.GetComponentInChildren<BulletScript>();
                    bulletScript.damageAmount = bulletDamage;
                    bulletScript.SetMaxRange(bulletRange);

                    //Vector3 cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 direction = nearsestEnemy.GetComponent<Transform>().position - transform.position;
                    direction.Normalize();
                    bullet.velocity = direction * 250;       
            }
        }

        if (timerRunning)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timeLeft = 0;
                timerRunning = false;
                GameObject.Find("map1").GetComponent<GameScript>().nrCatapults--;
                spawnPoint.NotifyTowerDestroy();
            }

            GetComponentInChildren<Slider>().value = timeLeft * 100 / timeout;
            GetComponentInChildren<Text>().text = ((int)timeLeft).ToString() + "s";
        }
    }

    public void SetSpawnPoint(SpawnPointScript spawn)
    {
        spawnPoint = spawn;
    }

    public void SetTimeout(float time)
    {
        timeout = time;
        timeLeft = time;
        timerRunning = true;
    }
}
