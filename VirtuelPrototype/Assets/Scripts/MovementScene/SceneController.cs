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


    private void Start()
    {
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
        }
        if (_camRoot != null)
        {
            Camera camera = _camRoot.GetComponentInChildren<Camera>();
            if (camera)
            {
                GameObject cameraGameObjekt = camera.gameObject;
                if (cameraGameObjekt)
                {
                    CameraController cameraController = cameraGameObjekt.AddComponent<CameraController>();
                    GameObject character = null;
                    if (_sceneRoot)
                    {
                        for (int i = 0; i < _sceneRoot.transform.childCount; i++)
                        {
                            GameObject child = _sceneRoot.transform.GetChild(i).gameObject;

                            if (child.name == "Character")
                            {

                                character = child;
                            }
                        }
                    }
                    if (character)
                    {
                        cameraController.HandleNewTarget(character);
                    }
                }
            }
        }
    }
    private void Update()
    {

    }

}
