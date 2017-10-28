using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int movespeed = 1;
    public Vector3 userDirection = Vector3.right;
    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        transform.Translate(userDirection * movespeed * Time.deltaTime);

        anim.SetFloat("MoveX", 1);
    }
}
