using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Cinemachine;

public class SplitSelectionController : Controller
{
    [SerializeField]
    private GameObject _selectionUI = null;
    [SerializeField]
    private GameObject _characterPreview = null;
    [SerializeField]
    private GameObject _button = null;
    [SerializeField]
    private List<GameObject> _createdButtons = new List<GameObject>();
    public Action<String> SelectCharacter;
    public void InitCharacterSelection(List<CharacterData> data, Player player)
    {
        if (_selectionUI)
        {
            GameObject Cam = GameObject.Find("----CAM----");
            Cam.GetComponent<Transform>().GetChild(0).GetComponent<CinemachineFreeLook>().enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            _selectionUI.GetComponent<RingHandler>().setCharacterData(data, this);
            _selectionUI.SetActive(true);
        }
    }
    private void SetGamePause()
    {

    }
    public void HandleCharacterSelection(String name)
    {
        if (_selectionUI)
        {
            foreach (GameObject b in _createdButtons)
            {
                Destroy(b);
            }
            _selectionUI.SetActive(false);
            GameObject Cam = GameObject.Find("----CAM----");
            Cam.GetComponent<Transform>().GetChild(0).GetComponent<CinemachineFreeLook>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        SelectCharacter?.Invoke(name);
        _characterPreview.GetComponent<CharacterPreview>().ActualizePreview();
    }
}
