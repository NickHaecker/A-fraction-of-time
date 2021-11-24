using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

[Serializable]
public class GameController : MonoBehaviour
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
    public List<SceneData> _scenes = new List<SceneData>();
    [SerializeField]
    public String _activeScene = "MovementScene";

    // [SerializeField]
    // public Character _character = null;

    [SerializeField]
    private String _controllerRootName = "";

    public Action<String> OnChangeScene;

    void Start()
    {
        OnChangeScene += HandleChangeScene;
        DontDestroyOnLoad(this);
        if (OnChangeScene != null)
        {
            OnChangeScene?.Invoke(_activeScene);
        }

    }
    private void HandleChangeScene(String scene)
    {
        _activeScene = scene;
        SceneManager.LoadSceneAsync(scene);
        // SceneManager.sceneLoaded += HandleLoadSceneController;
    }
    // private void HandleLoadSceneController(Scene scene, LoadSceneMode mode)
    // {
    //     if (scene != null)
    //     {
    //         GameObject[] rootObjects = scene.GetRootGameObjects();
    //         foreach (GameObject gameObject in rootObjects)
    //         {
    //             if (gameObject.name == _controllerRootName)
    //             {
    //                 SceneController sceneController = gameObject.GetComponent<SceneController>();
    //                 if (sceneController)
    //                 {
    //                     sceneController.OnInitSceneWithData += sceneController.HandleDataInit;
    //                     sceneController.OnInitSceneWithData?.Invoke(GetSceneDataOfActiveScene(), this.GetComponent<GameController>());
    //                 }
    //             }


    //         }
    //     }
    // }
    // private SceneData GetSceneDataOfActiveScene()
    // {
    //     SceneData data = null;
    //     foreach (SceneData sceneData in _scenes)
    //     {
    //         if (sceneData.NAME == _activeScene)
    //         {
    //             data = sceneData;
    //         }
    //     }
    //     return data;
    // }

}
