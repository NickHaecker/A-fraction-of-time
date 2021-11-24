using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : Controller
{
    // [SerializeField]
    // private MovementController _movementController = null;
    // [SerializeField]
    // private TimeController _timeController = null;
    // [SerializeField]
    // private string _name = null;
    // [SerializeField]
    // private bool _isAlive = true;
    // [SerializeField]
    // private AbilityController _abilityController = null;
    // [SerializeField]
    // private CharacterData _characterData = null;

    // // private GameObject _root = null;
    // private GameObject _cam = null;
    [SerializeField]
    private int _currentPlayableCharacter = 0;
    [SerializeField]
    private List<CharacterData> _playableCharacter = null;
    public Action BeforeCreateCharacter;
    public Action AfterCreateCharacter;
    public Action<Interaction> BeforeSplit;
    public Action<Interaction> AfterSplit;
    private void Start()
    {
        BeforeCreateCharacter += OnBeforeCreateCharacter;
        AfterCreateCharacter += OnAfterCreateCharacter;
        // Transform transform = new Transform();
        GameObject empty = new GameObject();
        empty.transform.position = new Vector3(-16.78f, 8.84f, -7.31f);
        this.InitPlayer(GetCurrentCharacterData(), empty.transform);


        // _movementController = gameObject.AddComponent<MovementController>();
        // _movementController.HandleSelectCamera(_cam);
        // // _movementController.HandleInitGround(_root);
        // _timeController = gameObject.AddComponent<TimeController>();
    }
    private void FixedUpdate()
    {

        this.HandleInput();
    }
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {

        }
    }
    private void HandleInteractions(Interaction interaction)
    {
        GetCurrentPlayer().InertInteractions(interaction);
    }
    private void OnAfterCreateCharacter()
    {
        Ability[] abilitys = GetCurrentPlayer().gameObject.GetComponents<Ability>();
        foreach (Ability ability in abilitys)
        {
            ability.CreateInteraction += HandleInteractions;
        }
    }
    private void OnBeforeCreateCharacter()
    {
        if (GetCurrentPlayer() != null)
        {
            Ability[] abilitys = GetCurrentPlayer().gameObject.GetComponents<Ability>();
            foreach (Ability ability in abilitys)
            {
                ability.CreateInteraction -= HandleInteractions;
            }
        }
    }
    public void HandleSplit(Interaction interaction)
    {
        BeforeSplit?.Invoke(interaction);
        AfterSplit?.Invoke(interaction);
    }
    public bool IsCurrentCharacterSplited()
    {
        return GetCurrentCharacterData().IS_SPLIT_REALITY_ORIGIN;
    }
    public CharacterData GetCurrentCharacterData()
    {
        if (_playableCharacter != null)
        {
            return _playableCharacter[_currentPlayableCharacter];
        }
        return null;
    }
    public Player GetCurrentPlayer()
    {
        return this.gameObject.transform.GetChild(_currentPlayableCharacter).gameObject.GetComponent<Player>();
    }
    public void InitPlayer(CharacterData character, Transform spawn)
    {
        BeforeCreateCharacter?.Invoke();
        if (character != null)
        {
            if (this.gameObject.transform.childCount > 0)
            {
                for (int i = 0; i < this.gameObject.transform.childCount; i++)
                {
                    Player player = this.gameObject.transform.GetChild(i).gameObject.GetComponent<Player>();
                    if (player && player.GetName() != character.name)
                    {
                        GameObject instantiation = Instantiate(character.PREFAB, spawn.position, character.PREFAB.transform.rotation, this.gameObject.transform);
                        player = instantiation.AddComponent<Player>();
                        player.Init(character);
                    }
                    // else if(player && player.GetName())

                    // GameObject player = this.gameObject.transform.GetChild(i).gameObject;
                    // if(player.name != character.PREFAB.name){
                    //     GameObject instantiation = Instantiate(character.PREFAB,spawn,,character.PREFAB.transform.rotation,this.gameObject);
                    //     instantiation.AddComponent<Player>();
                    // }

                }
            }
        }
        AfterCreateCharacter?.Invoke();
    }
    // public void ReconstructRecord()
    // {

    // }
    // public void InitPlayer(CharacterData data)
    // {
    //     // _characterData = data;

    // }
    // public void Ref(GameObject cam)
    // {
    //     // _root = root;
    //     _cam = cam;
    // }
    // public CharacterData GetCharacterData()
    // {
    //     return _characterData;
    // }

}
