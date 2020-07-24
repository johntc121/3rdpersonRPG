using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{

    private Animator animator;
    private CharacterController characterController;

    public float moveSpeed = 10f;
    public float rotationSpeed = 240f;
    private float gravity = 20f;

    private Vector3 moveDirection = Vector3.zero;

    private float horizontal;
    private float vertical;
    // Start is called before the first frame update
    void Start()
    {  
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical"); 

        //Limit to forward direction
        if(vertical < 0){
            vertical = 0;
        }

        transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

        if(characterController.isGrounded) {
            bool move = (vertical > 0) || (horizontal != 0);
            animator.SetBool("Run", move);

            moveDirection = Vector3.forward * vertical;
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
        }

        moveDirection.x = gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
