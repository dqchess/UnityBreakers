using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // TODO a better solution would be to ignore collision by layer
        GameObject[] objects = GameObject.FindGameObjectsWithTag("NoBulletCollision");

        foreach (GameObject obj in objects)
        {
            Collider2D selfCollider = GetComponent<Collider2D>();
            foreach(Collider2D collider in obj.GetComponents<Collider2D>()) { 
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

    }
}
