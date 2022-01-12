using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class SplitSelectionController : Controller
{
    [SerializeField]
    private GameObject _selectionUI = null;
    [SerializeField]
    private GameObject _button = null;
    [SerializeField]
    private List<GameObject> _createdButtons = new List<GameObject>();
    public Action<String> SelectCharacter;
    public void InitCharacterSelection(List<CharacterData> data,Player player)
    {
        if(_selectionUI)
        {
            Cursor.lockState = CursorLockMode.None;
            _selectionUI.GetComponent<RingHandler>().setCharacterData(data,this);
            _selectionUI.SetActive(true);
        }
    }
    private void SetGamePause()
    {

    }
    public void HandleCharacterSelection(String name)
    {
        if(_selectionUI)
        {
            foreach(GameObject b in _createdButtons)
            {
                Destroy(b);
            }
            _selectionUI.SetActive(false);
        }
        SelectCharacter?.Invoke(name);
    }
}
