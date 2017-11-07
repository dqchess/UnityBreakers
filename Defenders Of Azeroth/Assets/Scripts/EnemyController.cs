using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public int movespeed = 1;
    public Vector3 userDirection = Vector3.right;
    private Animator anim;
    private Vector3 animatorMovement;

    private float enemyHitDamage = 5f;
    private float enemyMaxHealth = 100f;
    private float enemyCurrentHealth = 100f;

    private Quaternion myRotation = Quaternion.identity;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        //transform.Translate(userDirection * movespeed * Time.deltaTime);

        //anim.SetFloat("MoveX", 1);

        // TODO change this with mainBase position after paths are done
        // var baseDirection = GameObject.Find("mainBase").transform.position;
        //Vector3 cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = transform.parent.transform.rotation * Vector3.forward;

        direction.Normalize();
        //GetComponent<Rigidbody2D>().velocity = direction * 50;

        Animate(direction);
    }

    void Rotate(float angles)
    {
        myRotation = Quaternion.Euler(new Vector3(0, angles, 0));
    }

    void Animate(Vector3 direction)
    {
        float angle = Vector3.SignedAngle(direction, Vector3.right, Vector3.forward);
        if (Mathf.Abs(angle) <= 45)
        {
            // right
            anim.SetFloat("MoveY", 0);
            anim.SetFloat("MoveX", +2);
            if (animatorMovement != Vector3.right)
            {
                //if (animatorMovement == Vector3.left)
                  //  Rotate(180);
                animatorMovement = Vector3.right;
            }
        }
        else if (Mathf.Abs(angle) > 90)
        {
            //
            anim.SetFloat("MoveY", 0);
            anim.SetFloat("MoveX", -2);
            if (animatorMovement != Vector3.left)
            {
                //Rotate(180);
                animatorMovement = Vector3.left;
            }
        }
        else if (angle > 45 && angle <= 90)
        {
            //down
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", -2);
        }
        else
        {
            //up
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", +2);
        }
    }

    private void LateUpdate()
    {
        transform.rotation = myRotation;
    }

    public float GetHitDamage()
    {
        return enemyHitDamage;
    }

    public void InflictDamage(float damage)
    {
        enemyCurrentHealth -= damage;

        if (enemyCurrentHealth <= 0)
        {
            enemyCurrentHealth = 0;
            Destroy(gameObject);
        }

        GetComponentInChildren<Slider>().value = enemyCurrentHealth * 100 / enemyMaxHealth;
    }

    // TODO this should be used in GameScript -> wave generation
    public void SetHealth(float health)
    {
        enemyCurrentHealth = health;
        enemyMaxHealth = health;
    }

    // TODO this shoud be used in GameScript -> wave generation
    public void  SetHitDamage(float damage)
    {
        enemyHitDamage = damage;
    }
}
