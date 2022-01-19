using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SceneController : Controller
{

    [SerializeField]
    private GameObject _controllerRoot = null;

    [SerializeField]
    private GameObject _camRoot = null;
    [SerializeField]
    private SceneData _sceneData = null;
    [SerializeField]
    private GameObject _playerRoot = null;

    [SerializeField]
    private static SceneController _instance = null;
    [SerializeField]
    public static SceneController Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {

        if(_playerRoot != null && _camRoot != null)
        {
            CameraController cameraController = _camRoot.GetComponentInChildren<CameraController>();
            PlayerController playerController = _playerRoot.GetComponent<PlayerController>();
            List<string> names = new List<string>();
            foreach(CharacterData data in playerController.GetCharacterData())
            {
                names.Add(data.NAME);
            }
            StateManager.DeleteData(names);
            playerController.AfterCharacterCreated += cameraController.HandleCreatePlayerCharacter;
            playerController.HandleStart();
        }
    }
    public GameObject GetCamGameObject()
    {
        return _camRoot.GetComponentInChildren<Camera>().gameObject;
    }
}
