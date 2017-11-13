using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBulletController : MonoBehaviour {

    public float damageAmount = 5f;
    private float maxRange = 99999f;
    private Vector3 startingPosition;

    // Use this for initialization
    void Start()
    {
        // TODO a better solution would be to ignore collision by layer
        GameObject[] objects = GameObject.FindGameObjectsWithTag("NoBulletCollision");

        foreach (GameObject obj in objects)
        {
            Collider2D selfCollider = GetComponent<Collider2D>();
            foreach (Collider2D collider in obj.GetComponents<Collider2D>())
            {
                Physics2D.IgnoreCollision(collider, GetComponent<Collider2D>());
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //if traveled distance is larger than the range -> destroy bullet

        if (Vector3.Distance(startingPosition, transform.position) > maxRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Defender_Controller defender = collision.gameObject.GetComponentInChildren<Defender_Controller>();
        
        if (!defender)
            return;


        defender.InflictDamage(damageAmount);
        Destroy(gameObject);
    }

    public void SetMaxRange(float range)
    {
        maxRange = range;
        startingPosition = transform.position;
    }
}
