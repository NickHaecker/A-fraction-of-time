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

    // Update is called once per frame
    void Update()
    {
        if (!_isGhostMode)
        {
            horizontal = Input.GetAxis("Horizontal") * 6;
            vertical = Input.GetAxis("Vertical") * 6;

            animator.SetFloat("speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));


            /* if (Input.GetKeyDown(KeyCode.Space))
             {
                 animator.SetBool("isJumping", true);
             }*/
        }
        else
        {
            if (_newDataAvailable)
            {
                horizontal = (_newPosition.x - _oldPosition.x) * 50;
                vertical = (_newPosition.z - _oldPosition.z) * 50;
                Debug.Log(horizontal + " / " + vertical);
                animator.SetFloat("speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
                _oldPosition = new Vector3(_newPosition.x, _newPosition.y, _newPosition.z);
                _newDataAvailable = false;
            }

        }

    }

    public void jump()
    {
        animator.SetBool("isJump", true);
        Debug.Log("jump true");
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
