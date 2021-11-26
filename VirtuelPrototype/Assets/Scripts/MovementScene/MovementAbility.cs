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

    private void Start()
    {
        playerTransform = this.gameObject.transform;
        controller = this.gameObject.GetComponent<CharacterController>();
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
            SubmitAction(InteractionType.walk, this.gameObject.GetComponent<Player>(), null, this.gameObject.transform, new Time());


        }
    }


    protected override void HandleCollisionEnter(Collider other)
    {
        // throw new System.NotImplementedException();
    }
    protected override void HandleCollisionExit(Collider other)
    {
        // throw new System.NotImplementedException();
    }
}
