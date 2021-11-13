using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SceneController : Controller
{
    
    [SerializeField]
    private GameObject _controllerRoot = null;

   private void Start() {
         if(_controllerRoot == null){
            _controllerRoot = this.gameObject;
        }
        if(_sceneRoot != null){
            Controller[] controller = _controllerRoot.GetComponentsInChildren<Controller>();
            foreach(Controller c in controller){
                c.InitSceneRoot(_sceneRoot);
            }
        }
   }
    private void Update() {
        
    }

}
