using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAbility : Ability
{

    [SerializeField]
    private float _speed = 6;
    private float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private Transform _playerTransform;
    private CharacterController _controller;
    private Transform _cam;

    private void Start()
    {
        _playerTransform = this.gameObject.transform;
        _controller = this.gameObject.GetComponent<CharacterController>();
        _cam = SceneController.Instance.GetCamGameObject().transform;
    }

    protected override void HandleInput()
    {
        // cam = Camera.main.gameObject.transform;
        //Player Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(_playerTransform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            _playerTransform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir.normalized * _speed * Time.deltaTime);
            SubmitAction(InteractionType.WALK, this.gameObject.GetComponent<Player>(), null, this.gameObject.transform, new Time());


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
