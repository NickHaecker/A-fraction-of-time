using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6;
    public float gravity = -9.81f;
    public float jumpHeight = 0.1f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Vector3 fallVelocity;
    public Transform groundCheck;
    public float groundDistance = 0.01f;
    public LayerMask groundMask;
    bool isGrounded;

    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        //Gravity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && fallVelocity.y < 0)
        {
            fallVelocity.y = -2f;
        }
        fallVelocity.y += gravity * Time.deltaTime;
        controller.Move(fallVelocity * Time.deltaTime);

        //Player Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }

        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            fallVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }



    }
}
