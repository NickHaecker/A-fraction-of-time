using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFIT_CharacterController : CharacterController
{
    [SerializeField]
    private float _gravity = -9.81f;
    
    [SerializeField]
    private Vector3 _fallVelocity;
    
    [SerializeField]
    private bool _isGrounded = false;

    [SerializeField]
    private float groundDistance;

    [SerializeField]
    private LayerMask groundMask;

    private GameObject groundCheck;
    // Start is called before the first frame update
    void Start()
    {
        groundCheck = new GameObject();
        Vector3 groundCheckPosition = new Vector3(0, this.height / 2 * -1, 0);
        groundCheck.transform.position = groundCheckPosition;

    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundDistance, groundMask);
        if (_isGrounded && _fallVelocity.y < 0)
        {
            _fallVelocity.y = -2f;
        }
        _fallVelocity.y += _gravity * Time.deltaTime;
        this.Move(_fallVelocity * Time.deltaTime);
        
    }
}
