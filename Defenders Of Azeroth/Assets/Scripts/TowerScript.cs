﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour {
    public Rigidbody2D bulletRigidbody;
    public GameObject healthBar;

    private SpawnPointScript spawnPoint = null;

    private float timeLeft;
    private bool timerRunning = false;

	// Use this for initialization
	void Start () {
        //healthBar.transform.SetParent(transform, false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            Rigidbody2D bullet = Instantiate(bulletRigidbody, transform.position, Quaternion.identity) as Rigidbody2D;

            Vector3 cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = cursorInWorldPos - transform.position;
            direction.Normalize();
            bullet.velocity = direction * 500;
        }

        if (timerRunning)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timerRunning = false;
                spawnPoint.NotifyTowerDestroy();
            }
        }
    }

    public void SetSpawnPoint(SpawnPointScript spawn)
    {
        spawnPoint = spawn;
    }

    public void SetTimeout(float time)
    {
        timeLeft = time;
        timerRunning = true;
    }
}
