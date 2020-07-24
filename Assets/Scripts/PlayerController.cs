using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 10f;
    private Rigidbody rb;

    private Vector3 movePlayer;
    private Vector3 moveVelocity;

    private float xMovement;
    private float zMovement;

    public Animator animator;

    private bool attack = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
         
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetBool("Attacking", attack);

        PlayerAttack();

    }


    void FixedUpdate() {
        PlayerMovement();
    }


    void PlayerMovement() {
        xMovement = Input.GetAxis("Horizontal");
        zMovement = Input.GetAxis("Vertical");


        movePlayer = new Vector3 (xMovement, 0f, zMovement);



        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        Quaternion camRotationFlattened = Quaternion.LookRotation(camForward);
        movePlayer = camRotationFlattened * movePlayer;

        if(xMovement != 0 || zMovement != 0){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movePlayer), 0.15f);
        }

        moveVelocity = movePlayer * moveSpeed;

        rb.velocity = moveVelocity;

        bool runAnim = (xMovement != 0) || (zMovement != 0);
        animator.SetBool("Run", runAnim);
    }


    void PlayerAttack() {
        if(Input.GetMouseButtonDown(0)){
            attack = true;
        }
        else{
            attack = false;
        }
    }
}
