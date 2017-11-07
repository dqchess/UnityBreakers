using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour {

    private float elapsedTime = 0;
    private float targetTime = 1f;
    bool gameEnded = false;


	// Use this for initialization
	void Start () {
        gameEnded = false;
        elapsedTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameEnded)
            return;

        if (GetComponentInChildren<Slider>().value == 0)
        {
            // TODO -> add transition to the end scene -> Scoreboard & stuff like that
            Debug.Log("GAME ENDED!");
            gameEnded = true;
        }

        if (elapsedTime >= targetTime)
        {
            elapsedTime = 0;
        }

        elapsedTime += Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (gameEnded)
            return;

        // test only collisions with enemies
        EnemyController enemy = collision.gameObject.GetComponentInChildren<EnemyController>();
        if (!enemy)
            return;

        if (elapsedTime >= targetTime)
        {
            //apply damage to the base
            GetComponentInChildren<Slider>().value -= enemy.GetHitDamage();
        }
    }
}
