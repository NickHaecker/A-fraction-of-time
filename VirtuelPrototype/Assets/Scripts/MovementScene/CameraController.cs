using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Controller
{
    [SerializeField]
    private Transform target = null;

    [SerializeField]
    private Vector3 offset = new Vector3(0, 2, -3);

    [SerializeField]
    private Vector3 lookatOffset = new Vector3(0, 1, 0);

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private bool follow = true;

    [SerializeField]
    private bool lookat = true;

    [SerializeField]
    private float lookatSpeed = 2f;




    void FixedUpdate()
    {
        if (!target)
        {
            return;
        }

        if (follow)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, speed * Time.deltaTime);
        }


        if (lookat)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookatSpeed * Time.deltaTime);
        }
    }

    public void HandleNewTarget(GameObject newTarget)
    {
        MovementController movementController = newTarget.GetComponent<MovementController>();
        movementController.HandleSelectCamera(this.gameObject);
        target = newTarget.transform;
    }

    public void HandleNewOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }
}
