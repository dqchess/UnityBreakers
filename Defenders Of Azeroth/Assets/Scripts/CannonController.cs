﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonController : MonoBehaviour {

	    public Rigidbody2D bulletRigidbody;
    public GameObject healthBar;

    private SpawnPointScript spawnPoint = null;

    private float timeLeft = 0f;
    private float timeout = 0f;
    private bool timerRunning = false;
    public float bulletDamage = 10f;
    public float bulletRange = 300f;
    private float fireFrequencySeconds = 1f;
    

    private float fireTimerElapsed = 0;

	// Use this for initialization
	void Start () {
        //healthBar.transform.SetParent(transform, false);
    }
	
	// Update is called once per frame
	void Update () {

        // TODO should fire once every 1 second -> the frequency could be increased as an update
        fireTimerElapsed += Time.deltaTime;

        if (fireTimerElapsed >= fireFrequencySeconds)
        {
            fireTimerElapsed = 0f;

            GameObject nearestDefender = GameObject.Find("map1").GetComponent<GameScript>().GetNearestDefender(transform.position, bulletRange);
            if (nearestDefender)
            {
                Rigidbody2D bullet = Instantiate(bulletRigidbody, transform.position, Quaternion.identity) as Rigidbody2D;
                CannonBulletController bulletScript = bullet.gameObject.GetComponentInChildren<CannonBulletController>();
                bulletScript.damageAmount = bulletDamage;
                bulletScript.SetMaxRange(bulletRange);

                //Vector3 cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = nearestDefender.GetComponent<Transform>().position - transform.position;
                direction.Normalize();
                bullet.velocity = direction * 500;
                
            }
        }

        if (timerRunning)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timeLeft = 0;
                timerRunning = false;
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
