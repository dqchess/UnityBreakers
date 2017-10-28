using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender_Controller : MonoBehaviour {

    public static int movespeed = 10;
    public Vector3 userDirection = Vector3.left;
    private Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
        transform.Rotate(0, 180, 0);
    }
    public void Update()
    {
       
        transform.Translate(userDirection * movespeed * Time.deltaTime, Space.Self);
        anim.SetFloat("MoveX", -2);
    }
}
