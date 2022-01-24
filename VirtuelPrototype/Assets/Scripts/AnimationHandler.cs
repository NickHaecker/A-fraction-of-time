using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator animator;

    private float horizontal, vertical;

    private bool _isGhostMode = false;
    private bool _newDataAvailable = false;
    private Vector3 _newPosition = new Vector3(0f,0f, 0f);
    private Vector3 _oldPosition = new Vector3(0f, 0f, 0f);
    private const float _speed = 6.0f;
    private const float _animationResetTimer = 0.3f;
    private float _lastAnimTimeStamp = 0f;

    // Update is called once per frame
    void Update()
    {
        if (!_isGhostMode)
        {
            horizontal = Input.GetAxis("Horizontal") * _speed;
            vertical = Input.GetAxis("Vertical") * _speed;

            animator.SetFloat("speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        }
        else
        {
            if (_newDataAvailable)
            {
                Vector3 direction = new Vector3(_newPosition.x - _oldPosition.x, 0, _newPosition.z - _oldPosition.z);
                direction = direction.normalized;
                animator.SetFloat("speed", Mathf.Abs(direction.x * _speed) + Mathf.Abs(direction.z * _speed));
                _oldPosition = new Vector3(_newPosition.x, _newPosition.y, _newPosition.z);
                _newDataAvailable = false;
                _lastAnimTimeStamp = TimeController.Instance.GetGameTime();
            }
            else if(_lastAnimTimeStamp > 0f && (_lastAnimTimeStamp + _animationResetTimer) <= TimeController.Instance.GetGameTime())
            {
                animator.SetFloat("speed", 0f);
            }

        }

    }

    public void jump()
    {
        animator.SetBool("isJump", true);
    }

    public void stopJump()
    {
        animator.SetBool("isJump", false);
    }
    public void SetGhostMode(bool active)
    {
        _isGhostMode = active;
    }
    public void SetGhostPosition(Vector3 position)
    {
        _newPosition = position;
        _newDataAvailable = true;
    }
}
