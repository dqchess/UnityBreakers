using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Defender_Controller : MonoBehaviour {

    public static int movespeed = 10;
    private Vector3 userDirection = Vector3.left;

    private Animator anim;
    private Vector3 animatorMovement;

    private float elapsedTime = 0f;
    private float findEnemyTargetTime = 10f;
    public float damageAmount = 5f;

    private float defenderMaxHealth = 120f;
    public float defenderCurrentHealth = 120f;

    private Quaternion myRotation = Quaternion.identity;

    private GameObject targetEnemy = null;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        transform.rotation = myRotation;
    }

    public void Update()
    {
        //Vector3 cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // TODO do this check once every 3 seconds or if there is no enemy already selected as target
        elapsedTime += Time.deltaTime;
        
        // set AI direction
        if (!targetEnemy || elapsedTime >= findEnemyTargetTime)
        {
            GameObject enemy = GameObject.Find("map1").GetComponent<GameScript>().GetNearestEnemy(transform.position, 99999f);
            if (enemy)
            {
                transform.parent.GetComponent<AIPath>().target = enemy.GetComponent<Transform>();
                transform.parent.GetComponent<AIPath>().TrySearchPath();
            }

            elapsedTime = 0;
        }

        Vector3 direction = transform.parent.transform.rotation * Vector3.forward;
        direction.Normalize();
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
                if (animatorMovement == Vector3.left)
                  Rotate(180);
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
                Rotate(180);
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyController enemy = collision.gameObject.GetComponentInChildren<EnemyController>();

        if (!enemy)
            return;

        enemy.InflictDamage(damageAmount * Time.deltaTime);
        InflictDamage(enemy.GetHitDamage() * Time.deltaTime);
    }

    public void InflictDamage(float damage)
    {
        defenderCurrentHealth -= damage;

        if (defenderCurrentHealth <= 0)
        {
            defenderCurrentHealth = 0;
            GameObject.Find("map1").GetComponent<GameScript>().NotifyDefenderDestroy(gameObject);
        }

        GetComponentInChildren<Slider>().value = defenderCurrentHealth * 100 / defenderMaxHealth;
    }

    public void NotifyEnemyDead(GameObject enemy)
    {
        if (targetEnemy == enemy) { 
            targetEnemy = null;
            Debug.Log("EnemyDead");
        }
    }
}
