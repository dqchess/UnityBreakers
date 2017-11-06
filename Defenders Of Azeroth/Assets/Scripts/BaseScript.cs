using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour {

    private float elapsedTime = 0;
    private float targetTime = 1f;



	// Use this for initialization
	void Start () {
        Debug.Log(GetComponentInChildren<Slider>().value);
	}
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime >= targetTime)
        {
            elapsedTime = 0;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        EnemyController enemy = collision.gameObject.GetComponentInChildren<EnemyController>();
        if (!enemy)
            return;

        if (elapsedTime >= targetTime)
        {

        }
    }
}
