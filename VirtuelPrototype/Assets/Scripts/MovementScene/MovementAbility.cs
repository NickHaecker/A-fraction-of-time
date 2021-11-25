using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAbility : Ability
{
    
    [SerializeField]
    private float speed = 6;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    public Transform playerTransform;
    public CharacterController controller;
    public Transform cam; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        this.NAME = "MovementAbility";
        this.DESCRIPTION = "Allows the character to move by using WASD-Keys"; 
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    protected override void HandleInput()
    {
        cam = Camera.main.gameObject.transform;
        //Player Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(playerTransform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            playerTransform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }
    }

    protected override void HandleCollision(GameObject other)
    {
        throw new System.NotImplementedException();
    }
}
