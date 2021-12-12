using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator animator;

    private float horizontal, vertical;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * 6;
        vertical = Input.GetAxis("Vertical") * 6;

        animator.SetFloat("speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isJumping", true);
        }

        // if(Input.GetKeyUp(KeyCode.Space))
        // {
        //     animator.SetBool("isJumping", false);
        // }
    }
}
