using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

[Serializable]
public class GameData : MonoBehaviour
{
    [SerializeField]
    private static PlayerData _data;

    [SerializeField]
    public static PlayerData Player
    {
        get
        {
            if (_data == null)
            {
                _data = new PlayerData();
            }
            return _data;
        }
    }
    [SerializeField]
    public List<String> _scenes = new List<String>();
    [SerializeField]
    public String _activeScene = "MovementScene";

    [SerializeField]
    public Character _character = null;

    [SerializeField]
    private String _controllerRootName = "";

    public Action<String> OnChangeScene;

    void Start()
    {
        OnChangeScene += OnHandleChangeScene;
        DontDestroyOnLoad(this);
        if (OnChangeScene != null)
        {
            OnChangeScene?.Invoke(_activeScene);
        }

    }
    private void OnHandleChangeScene(String scene)
    {
        _activeScene = scene;
        SceneManager.LoadSceneAsync(scene);
        SceneManager.sceneLoaded += OnInitSceneController;
    }
    private void OnInitSceneController(Scene scene, LoadSceneMode mode)
    {
        if (scene != null)
        {
            GameObject[] rootObjects = scene.GetRootGameObjects();
            foreach (GameObject gO in rootObjects)
            {
                if (gO.name == _controllerRootName)
                {
                    SceneController sceneController = gO.GetComponent<SceneController>();
                    if (sceneController)
                    {
                        sceneController.SetGameData(this);

                    }
                }


            }
        }
    }

}
