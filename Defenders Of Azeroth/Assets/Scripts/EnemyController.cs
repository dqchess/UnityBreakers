using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int movespeed = 1;
    public Vector3 userDirection = Vector3.right;
    private Animator anim;
    private Vector3 animatorMovement;

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
}
