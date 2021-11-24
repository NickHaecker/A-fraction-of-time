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
    private List<GameObject> _createdPlayer = new List<GameObject>();
    [SerializeField]
    private PlayerController _currentCharacter = null;

    public Action<SceneData, GameController> OnInitSceneWithData;

    public Action<GameObject> OnInstantiateCharacter;

    private GameController _gameController = null;

    public void HandleDataInit(SceneData data, GameController gameController)
    {
        _sceneData = data;
        _gameController = gameController;
    }

    private void Awake()
    {
        if (!_controllerRoot)
        {
            _controllerRoot = gameObject;
        }
    }
    private void InitController()
    {
        if (_sceneRoot)
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
                    cameraController.AddCammRootGameObject(_camRoot);
                    OnInstantiateCharacter += cameraController.HandleCreateCharacter;

                }
            }
        }
    }

    private void InitPlayer()
    {
        if (_sceneRoot)
        {
            GameObject spawn = GetChildInRoot(_sceneRoot, "Spawn");
            foreach (String name in GameController.Player._playableCharacter)
            {
                foreach (CharacterData ch in _gameController._character.CHARACTER)
                {
                    if (name == ch.NAME)
                    {
                        if (spawn)
                        {
                            Camera camera = _camRoot.GetComponentInChildren<Camera>();
                            GameObject cameraGameObjekt = camera.gameObject;
                            GameObject character = Instantiate(ch.PREFAB, spawn.transform.position, ch.PREFAB.transform.rotation, _sceneRoot.transform);
                            PlayerController cC = character.AddComponent<PlayerController>();
                            cC.InitPlayer(ch);
                            cC.Ref(cameraGameObjekt);
                            _currentCharacter = cC;
                            _createdPlayer.Add(character);
                            OnInstantiateCharacter?.Invoke(character);
                        }
                    }
                }
            }
        }
    }
    private GameObject GetChildInRoot(GameObject root, String child)
    {
        GameObject ch = null;
        for (int i = 0; i < root.transform.childCount; i++)
        {
            GameObject c = _sceneRoot.transform.GetChild(i).gameObject;

            if (c.name == child)
            {

                ch = c;
            }
        }
        return ch;
    }

    private void Start()
    {
        InitController();
        InitPlayer();

    }
    private void Update()
    {

    }
}
