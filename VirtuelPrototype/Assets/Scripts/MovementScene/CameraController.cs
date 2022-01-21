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
    CinemachineFreeLook _cinemachineFreeLook;

    private void Start()
    {
        _camRoot = this.gameObject;
        if(_camRoot)
        {
            _cinemachineFreeLook = _camRoot.GetComponentInChildren<CinemachineFreeLook>();
        }
    }

    private void HandleChangeNewTarget()
    {
        if(_cinemachineFreeLook)
        {
            _cinemachineFreeLook.Follow = _target;
            _cinemachineFreeLook.LookAt = _target;
        }
    }
    // public void InitCamera(PlayerController playerController){
    //     playerController.AfterCharacterCreated += HandleCreatePlayerCharacter;
    // }

    public void HandleCreatePlayerCharacter(Player player)
    {
        ;
        _target = player.gameObject.transform.Find("mixamorig:Hips").GetChild(2).GetChild(0).GetChild(0).GetChild(1);
        HandleChangeNewTarget();
    }
}
