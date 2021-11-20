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
    private List<GameObject> _character = new List<GameObject>();
    private GameData _gameData = null;

    public Action<GameObject> OnInstantiateCharacter;


    private void Start()
    {
        CameraController cameraController = null;
        GameObject character = null;
        GameObject spawn = null;
        MovementController movementController = null;
        if (_controllerRoot == null)
        {
            _controllerRoot = this.gameObject;
        }
        if (_sceneRoot != null)
        {
            Controller[] controller = _controllerRoot.GetComponentsInChildren<Controller>();
            foreach (Controller c in controller)
            {
                c.InitSceneRoot(_sceneRoot);
            }

            for (int i = 0; i < _sceneRoot.transform.childCount; i++)
            {
                GameObject child = _sceneRoot.transform.GetChild(i).gameObject;

                if (child.name == "Spawn")
                {

                    spawn = child;
                }
            }

            foreach (String name in GameData.Player._playableCharacter)
            {
                if (name == "Character")
                {
                    foreach (CharacterData ch in _gameData._character.CHARACTER)
                    {
                        if (name == ch.NAME)
                        {
                            if (spawn)
                            {
                                character = Instantiate(ch.PREFAB, spawn.transform.position, ch.PREFAB.transform.rotation, _sceneRoot.transform);
                                movementController = character.AddComponent<MovementController>();
                                movementController.HandleInitGround(_sceneRoot);
                            }
                        }
                    }
                }
            }
        }
        if (_camRoot != null)
        {
            Camera camera = _camRoot.GetComponentInChildren<Camera>();
            if (camera)
            {
                GameObject cameraGameObjekt = camera.gameObject;
                if (cameraGameObjekt)
                {

                    cameraController = cameraGameObjekt.AddComponent<CameraController>();
                    cameraController.AddCammRootGameObject(_camRoot);
                    if (character != null)
                    {
                        cameraController.HandleNewTarget(character);
                        movementController.HandleSelectCamera(cameraGameObjekt);
                    }
                }
            }
        }
    }
    private void Update()
    {

    }
    public void SetGameData(GameData data)
    {
        _gameData = data;
    }

}
