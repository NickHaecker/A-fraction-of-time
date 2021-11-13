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
    private float _sensitivity = 2f;
    [SerializeField]
    private Transform _camera;
    [SerializeField]
    private float _panSpeed = 6f;
    [SerializeField]
    private float _turnSmoothTime = 0.1f;
    [SerializeField]
    private float _turnSmoothVelocity;

    Vector3 _velocity;
    [SerializeField]
    private const float Y_ANGLE_MIN = -120.0f;
    [SerializeField]
    private const float Y_ANGLE_MAX = 80.0f;

    private float _currentX = 0.0f;
    private float _currentY = 0.0f;
    [SerializeField]
    private float _distance = 5.0f;

    private void Start()
    {
        _characterController = gameObject.AddComponent<CharacterController>();
        _boxCollider = gameObject.GetComponent<BoxCollider>();
        _groundMask = LayerMask.GetMask("Default");
    }
    private void Update()
    {
        if (IsGrounded() && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _characterController.Move(move * _speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
        _velocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_velocity * Time.deltaTime);

        if (_camera)
        {
            _currentX += Input.GetAxis("Mouse X") * _panSpeed;
            _currentY += Input.GetAxis("Mouse Y") * _panSpeed;

            _currentY = Mathf.Clamp(_currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

            Vector3 panDirection = new Vector3(0, 0, -_distance);
            Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0);
            _camera.position = transform.position + rotation * panDirection;

            _camera.LookAt(transform.position);

        }

    }
    public bool IsGrounded()
    {
        return Physics.CheckBox(_groundCheck.position, new Vector3(_boxCollider.size.x / 2, _boxCollider.size.y / 2, _boxCollider.size.z / 2), _boxCollider.transform.rotation, _groundMask);
        // return Physics.CheckSphere(_groundCheck.position, _groundDistance,_groundMask);
    }
    public void HandleInitGround(GameObject sceneRoot)
    {
        _sceneRoot = sceneRoot;
        if (_sceneRoot != null)
        {
            for (int i = 0; i < _sceneRoot.transform.childCount; i++)
            {
                GameObject child = _sceneRoot.transform.GetChild(i).gameObject;

                if (child.name == "Ground")
                {

                    _groundCheck = child.transform;
                }
            }
        }
    }
    public void HandleSelectCamera(GameObject cam)
    {
        _camera = cam.transform;
    }

}
