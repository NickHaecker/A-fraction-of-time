using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : Ability
{
    public CharacterController controller;
    
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private float jumpHeight = 2f;
    
    [SerializeField]
    private Vector3 fallVelocity;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundDistance = 0.2f;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        this.NAME = "JumpAbility";
        this.DESCRIPTION = "Allows the character to jump by using Space-Key"; 
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    protected override void HandleInput()
    {
        //Ground checking to see if agent is touching the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && fallVelocity.y < 0)
        {
            fallVelocity.y = -2f;
        }
        fallVelocity.y += gravity * Time.deltaTime;
        controller.Move(fallVelocity * Time.deltaTime);
        
        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            fallVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }
    }

    protected override void HandleCollision(GameObject other)
    {
        throw new System.NotImplementedException();
    }
}
