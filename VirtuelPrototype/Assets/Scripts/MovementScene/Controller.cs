using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// using UnityEngine.Events;
public abstract class Controller : MonoBehaviour
{
    [SerializeField]
    protected GameObject _sceneRoot = null;

    protected Action OnSceneRootInit;



    public void InitSceneRoot(GameObject sceneRoot){
        _sceneRoot = sceneRoot;
        OnSceneRootInit?.Invoke();
    }
}
