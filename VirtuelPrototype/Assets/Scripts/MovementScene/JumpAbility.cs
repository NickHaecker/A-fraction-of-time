using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private Vector3 _jumpDirection;

    private Transform _cam;

    private void Start()
    {
        _controller = this.gameObject.GetComponent<CharacterController>();
        _collider = this.gameObject.GetComponent<CapsuleCollider>();
        
        Physics.IgnoreCollision(GetComponent<CharacterController>(), GetComponent<CapsuleCollider>());
        _cam = SceneController.Instance.GetCamGameObject().transform;
    }

    protected override void HandleInput()
    {
        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _fallVelocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            this.JumpDirection();
            GetComponent<AnimationHandler>().jump();
            SubmitAction(InteractionType.JUMP, this.gameObject.GetComponent<Player>(), this.gameObject, this.gameObject.transform, TimeController.Instance.GetGameTime());
        } else
        {
            if(_isGrounded)
            {
                this.gameObject.GetComponent<AnimationHandler>().stopJump();
            }
            if(!_isGrounded)
            {
                _fallVelocity.y += _gravity * Time.deltaTime;
            }
        }
        if(!_isGrounded)
        {
            _controller.Move(_fallVelocity * Time.deltaTime);
        }
        
    }

    protected void JumpDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            _jumpDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
        } else
        {
            _jumpDirection = new Vector3(0f, 0f, 0f);
        }
    }

    public Vector3 GetJumpdirection()
    {
        return _jumpDirection;
    }


    protected override void HandleCollisionEnter(Collider other)
    {
        Debug.Log("Hitted:" + other.name);
        
        if (other.tag == "Ground")
        {
            _isGrounded = true;
        }

        if(GetComponent<Animator>().GetBool("isJump"))
        {
            GetComponent<Animator>().SetBool("isJump", false);
        }

        if (other.name == "infoPoint")
        {
            GameObject ui = GameObject.Find("----HUD----");
            ui.GetComponent<Transform>().GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
            ui.GetComponent<Transform>().GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = other.GetComponent<InfoPoint>()._info;
        }
        
    }
    protected override void HandleCollisionExit(Collider other)
    {
        if(other.tag == "Ground")
        {
            _isGrounded = false;
        }

        if (other.name == "infoPoint")
        {
            GameObject ui = GameObject.Find("----HUD----");
            ui.GetComponent<Transform>().GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);
        }
        // throw new System.NotImplementedException();
    }
}
