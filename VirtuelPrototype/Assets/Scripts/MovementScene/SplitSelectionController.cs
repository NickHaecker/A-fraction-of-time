using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SplitSelectionController : Controller
{
    [SerializeField]
    private GameObject _selectionUI = null;
    public Action<String> SelectCharacter;
    public void InitCharacterSelection(CharacterData data, Player player){

    }
    private void SetGamePause(){

    }
    public void HandleCharacterSelection(String name){
        
    }
}
