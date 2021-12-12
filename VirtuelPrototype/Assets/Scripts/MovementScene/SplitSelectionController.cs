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
    public void InitCharacterSelection(List<CharacterData> data, Player player)
    {

        if (_selectionUI)
        {
            _selectionUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            if (_button)
            {
                foreach (CharacterData d in data)
                {
                    if (player.GetCharacterData().NAME != d.NAME)
                    {
                        GameObject gO = Instantiate(_button, _selectionUI.transform.position, new Quaternion(0, 0, 0, 0), _selectionUI.transform);
                        Button button = gO.GetComponentInChildren<Button>();
                        button.GetComponentInChildren<Text>().text = d.NAME;
                        button.onClick.AddListener(() => HandleCharacterSelection(button.GetComponentInChildren<Text>().text));
                    }

                }
            }

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
        }
        SelectCharacter?.Invoke(name);
    }
}
