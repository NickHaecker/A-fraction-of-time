using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : Controller
{
    private MovementController _currentCharacter = null;

    private void Start() {
        Debug.Log(_sceneRoot);
        OnSceneRootInit += HandleSceneRootInit;
    }

    private void Update() {
        // Vector3 move = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        // _currentCharacter.HandleMovement(move);

        // if(Input.GetButtonDown("Jump") && _currentCharacter.isGrounded()){
        //     _currentCharacter.HandleJump();
        // }

    }

    private void HandleSceneRootInit(){
        if(_sceneRoot != null){
            for(int i = 0; i < _sceneRoot.transform.childCount;i++){
                GameObject child = _sceneRoot.transform.GetChild(i).gameObject;

                if(child.name == "Character"){
                    Debug.Log(1111);
                    _currentCharacter = child.AddComponent<MovementController>();
                    _currentCharacter.HandleInitGround(_sceneRoot);
                    // _currentCharacter.HandleInitGround();
                }
            }
        }
    }
}
