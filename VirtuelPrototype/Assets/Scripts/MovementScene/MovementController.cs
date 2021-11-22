using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : Controller
{
    [SerializeField]
    private CharacterController _characterController = null;
    [SerializeField]
    private BoxCollider _boxCollider = null;
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private float _gravity = -9.81f;
    [SerializeField]
    private float _jumpHeight = 3f;
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private float _groundDistance = 0.4f;
    [SerializeField]
    private LayerMask _groundMask;
    [SerializeField]
    private Transform _camera;
    [SerializeField]
    private float _turnSmoothTime = 0.1f;
    [SerializeField]
    private float _turnSmoothVelocity;




    Vector3 _velocity;

    private void Start()
    {
        _characterController = gameObject.AddComponent<CharacterController>();
        _boxCollider = gameObject.GetComponent<BoxCollider>();
        _groundMask = LayerMask.GetMask("Default");
    }
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _characterController.Move(moveDirection * _speed * Time.deltaTime);
        }
        // if (Input.GetButtonDown("Jump") && IsGrounded())
        // {
        //     _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        // }
        // _velocity.y += _gravity * Time.deltaTime;

        // _characterController.Move(_velocity * Time.deltaTime);


    }
    // public bool IsGrounded()
    // {
    //     return Physics.CheckBox(_groundCheck.position, new Vector3(_boxCollider.size.x / 2, _boxCollider.size.y / 2, _boxCollider.size.z / 2), _boxCollider.transform.rotation, _groundMask);
    // }
    // public void HandleInitGround(GameObject sceneRoot)
    // {
    //     _sceneRoot = sceneRoot;
    //     if (_sceneRoot != null)
    //     {
    //         for (int i = 0; i < _sceneRoot.transform.childCount; i++)
    //         {
    //             GameObject child = _sceneRoot.transform.GetChild(i).gameObject;

    //             if (child.name == "Ground")
    //             {

    //                 _groundCheck = child.transform;
    //             }
    //         }
    //     }
    // }
    public void HandleSelectCamera(GameObject cam)
    {
        _camera = cam.transform;
    }

}
