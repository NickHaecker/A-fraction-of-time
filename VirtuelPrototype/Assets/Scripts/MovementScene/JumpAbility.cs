using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : Ability
{
    public CharacterController _controller;

    [SerializeField]
    private float _gravity = -9.81f;
    [SerializeField]
    private float _jumpHeight = 2f;

    [SerializeField]
    private Vector3 _fallVelocity;
    [SerializeField]
    private bool _isGrounded = false;

    private void Start()
    {
        _controller = this.gameObject.GetComponent<CharacterController>();
    }

    protected override void HandleInput()
    {
        //Ground checking to see if agent is touching the ground
        // isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(_isGrounded && _fallVelocity.y < 0)
        {
            _fallVelocity.y = -2f;
        }
        _fallVelocity.y += _gravity * Time.deltaTime;
        _controller.Move(_fallVelocity * Time.deltaTime);

        //Jumping
        if(Input.GetButtonDown("Jump") && _isGrounded)
        {
            SubmitAction(InteractionType.JUMP,this.gameObject.GetComponent<Player>(),null,this.gameObject.transform,TimeController.Instance.GetGameTime());
            _fallVelocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

        }
    }


    protected override void HandleCollisionEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            _isGrounded = true;
        }
        // throw new System.NotImplementedException();
    }
    protected override void HandleCollisionExit(Collider other)
    {
        if(other.tag == "Ground")
        {
            _isGrounded = false;
        }
        // throw new System.NotImplementedException();
    }
}
