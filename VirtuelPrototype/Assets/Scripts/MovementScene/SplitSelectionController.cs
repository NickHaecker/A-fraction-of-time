using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Cinemachine;

public class SplitSelectionController : Controller
{
    [SerializeField]
    private GameObject _cam = null;
    [SerializeField]
    private GameObject _uI = null;
    [SerializeField]
    private GameObject _selectionUI = null;
    [SerializeField]
    private GameObject _characterPreview = null;
    [SerializeField]
    private GameObject _button = null;
    [SerializeField]
    private List<GameObject> _createdButtons = new List<GameObject>();
    
    
    public Action<String> SelectCharacter;

    private bool _firstSplit = true;
    public void InitCharacterSelection(List<CharacterData> data,Player player)
    {
        _cam.GetComponent<CameraController>()._cinemachineFreeLook.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _selectionUI.GetComponent<RingHandler>().setCharacterData(data, this);
        _selectionUI.SetActive(true);
        AudioPlayerScript.instance.playSpecificAudio("openUI");
    }

    public void CloseCharacterSelection()
    {
        _cam.GetComponent<CameraController>()._cinemachineFreeLook.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioPlayerScript.instance.playSpecificAudio("closeUI");
    }
    private void SetGamePause()
    {

    }
    public void HandleCharacterSelection(String name)
    {
        if(_firstSplit)
        {
            _firstSplit = false;
            _uI.GetComponent<UIHandler>().showRuntimeInfobox("Everything you do now with a character aside from the main one will be recorded. It will then be replayed once you merge back.", 15);
        }

        AudioPlayerScript.instance.playSpecificAudio("splitting");
        
        if(_selectionUI)
        {
            foreach(GameObject b in _createdButtons)
            {
                Destroy(b);
            }
            _selectionUI.SetActive(false);
            _cam.GetComponent<CameraController>()._cinemachineFreeLook.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        SelectCharacter?.Invoke(name);
        _characterPreview.GetComponent<CharacterPreview>().ActualizePreview();
    }
}
