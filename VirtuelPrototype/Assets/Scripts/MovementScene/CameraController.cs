using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : Controller
{
    [SerializeField]
    private GameObject _camRoot = null;
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    CinemachineFreeLook _cinemachineFreeLook;

    private void Start()
    {
        // _cinemachineFreeLook = 
        if (_camRoot)
        {
            _cinemachineFreeLook = _camRoot.GetComponentInChildren<CinemachineFreeLook>();
        }
        HandleChangeNewTarget();
    }
    void FixedUpdate()
    {

    }
    private void HandleChangeNewTarget()
    {
        if (_cinemachineFreeLook)
        {
            _cinemachineFreeLook.Follow = target;
            _cinemachineFreeLook.LookAt = target;
        }
    }

    public void HandleNewTarget(GameObject newTarget)
    {
        MovementController movementController = newTarget.GetComponent<MovementController>();
        movementController.HandleSelectCamera(this.gameObject);
        target = newTarget.transform;
        HandleChangeNewTarget();
    }
    public void AddCammRootGameObject(GameObject root)
    {
        _camRoot = root;
    }
    public void HandleNewOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }
}
