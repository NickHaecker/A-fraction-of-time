using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    [SerializeField]
    private MovementController _movementController = null;
    [SerializeField]
    private TimeController _timeController = null;
    [SerializeField]
    private string _name = null;
    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private AbilityController _abilityController = null;
    [SerializeField]
    private CharacterData _characterData = null;

    // private GameObject _root = null;
    private GameObject _cam = null;

    private void Start()
    {
        _movementController = gameObject.AddComponent<MovementController>();
        _movementController.HandleSelectCamera(_cam);
        // _movementController.HandleInitGround(_root);
        _timeController = gameObject.AddComponent<TimeController>();
    }
    public void InitPlayer(CharacterData data)
    {
        _characterData = data;

    }
    public void Ref(GameObject cam)
    {
        // _root = root;
        _cam = cam;
    }
    public CharacterData GetCharacterData()
    {
        return _characterData;
    }

}
