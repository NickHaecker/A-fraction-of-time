using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : Ability
{
    public CharacterController _controller;

    public CapsuleCollider _collider;

    [SerializeField]
    private float _gravity = -9.81f;
    [SerializeField]
    private float _jumpHeight = 2f;

    [SerializeField]
    private Vector3 _fallVelocity;
    [SerializeField]
    private bool _isGrounded = true;

    private void Start()
    {
        _controller = this.gameObject.GetComponent<CharacterController>();
        _collider = this.gameObject.GetComponent<CapsuleCollider>();
        
        Physics.IgnoreCollision(_controller,_collider);
    }

    protected override void HandleInput()
    {
        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        // (-0.5) change this value according to your character y position + 1
        {
            _fallVelocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            this.gameObject.GetComponent<AnimationHandler>().jump();
            //SubmitAction(InteractionType.JUMP, this.gameObject.GetComponent<Player>(), this.gameObject, this.gameObject.transform, TimeController.Instance.GetGameTime());
            
        } else
        {
            _fallVelocity.y += _gravity * Time.deltaTime;
            if(_isGrounded)
            {
                this.gameObject.GetComponent<AnimationHandler>().stopJump();
            }
            
        }
        _controller.Move(_fallVelocity * Time.deltaTime);
    


        //Ground checking to see if agent is touching the ground
        // isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        /*if(_isGrounded && _fallVelocity.y < 0)
        {
            _fallVelocity.y = -2f;
        }
        if(!_isGrounded && _fallVelocity.y < 0)
        {
            Debug.Log("fällt schneller");
            _fallVelocity.y += _gravity * Time.deltaTime;
        }

        _controller.Move(_fallVelocity * Time.deltaTime);

        //if (Input.GetKey(KeyCode.Space)) Debug.Log("Jump");

        //Jumping
        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            Debug.Log("jump");
            SubmitAction(InteractionType.JUMP,this.gameObject.GetComponent<Player>(),this.gameObject,this.gameObject.transform,TimeController.Instance.GetGameTime());
            _fallVelocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }*/
    }


protected override void HandleCollisionEnter(Collider other)
    {
        Debug.Log(other);
        
        if (other.tag == "Ground")
        {
            _isGrounded = true;
            Debug.Log("grounded");
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
