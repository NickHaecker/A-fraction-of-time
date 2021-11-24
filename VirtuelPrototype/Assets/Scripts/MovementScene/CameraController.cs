using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : Controller
{
    [SerializeField]
    private GameObject _camRoot = null;
    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    CinemachineFreeLook _cinemachineFreeLook;

    private void Start()
    {
        if (_camRoot)
        {
            _cinemachineFreeLook = _camRoot.GetComponentInChildren<CinemachineFreeLook>();
            HandleChangeNewTarget();
        }
    }
    void FixedUpdate()
    {

    }
    private void HandleChangeNewTarget()
    {
        if (_cinemachineFreeLook)
        {
            _cinemachineFreeLook.m_Follow = _target;
            _cinemachineFreeLook.m_LookAt = _target;
        }
    }

    public void AddCammRootGameObject(GameObject root)
    {
        _camRoot = root;
    }
    public void HandleNewOffset(Vector3 newOffset)
    {
        _offset = newOffset;
    }
    public void HandleCreateCharacter(GameObject character)
    {
        _target = character.transform;
        HandleChangeNewTarget();
    }
}
