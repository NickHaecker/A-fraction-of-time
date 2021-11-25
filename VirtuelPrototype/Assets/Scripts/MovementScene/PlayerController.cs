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
    private Player _currentCharacter = null;
    [SerializeField]
    private List<CharacterData> _playableCharacter = null;

    // public Change
    // public Action<Player> BeforeCreateCharacter;
    // public Action<Player> AfterCreateCharacter;
    // public Action BeforeSwitchCharacter;
    // public Action AfterSwitchCharacter;
    // public Action<Player> BeforeSplit;
    // public Action<Player> AfterSplit;
    // private CharacterData _target
    // private void Start()
    // {
    //     // BeforeSplit += StateManager.SaveRecordData;
    //     // BeforeSplit += OnBeforeCreateCharacter;
    //     // BeforeSplit += OnDeleteOldCharacter;
    //     // AfterSplit += OnAfterCreateCharacter;
    //     // AfterSplit += StateManager.LoadPlayer;
    //     // Transform transform = new Transform();
    //     // GameObject empty = new GameObject();
    //     // empty.transform.position = new Vector3(-16.78f, 8.84f, -7.31f);
    //     // this.InitPlayer(GetCurrentCharacterData(), empty.transform);


    //     // _movementController = gameObject.AddComponent<MovementController>();
    //     // _movementController.HandleSelectCamera(_cam);
    //     // // _movementController.HandleInitGround(_root);
    //     // _timeController = gameObject.AddComponent<TimeController>();
    // }
    // private void FixedUpdate()
    // {
    //     this.HandleInput();
    // }
    // public void HandleInput()
    // {
    //     // GameObject empty = null;
    //     // if (GetCurrentPlayer().name == "Character") { empty = new GameObject("Kaengu"); } else { empty = new GameObject("Character"); }
    //     if (Input.GetKeyDown(KeyCode.G))
    //     {
    //         // HandleSplit(new Interaction().Copy(InteractionType.change, GetCurrentPlayer(), empty, GetCurrentPlayer().gameObject.transform, new Time()));
    //     }
    // }
    // private void HandleInteractions(Interaction interaction)
    // {
    //     GetCurrentPlayer().InertInteractions(interaction);
    // }
    // private void OnAfterCreateCharacter(Player player)
    // {
    //     Ability[] abilitys = player.gameObject.GetComponents<Ability>();
    //     foreach (Ability ability in abilitys)
    //     {
    //         ability.CreateInteraction += HandleInteractions;
    //     }
    // }
    // // private void OnDeleteOldCharacter(Player player)
    // // {
    // //     if (player.GetCharacterData().IS_SPLIT_REALITY_ORIGIN)
    // //     {
    // //         Destroy(player.gameObject);
    // //     }
    // // }
    // private void OnBeforeCreateCharacter(Player player)
    // {
    //     Ability[] abilitys = player.gameObject.GetComponents<Ability>();
    //     foreach (Ability ability in abilitys)
    //     {
    //         ability.CreateInteraction -= HandleInteractions;
    //     }
    //     // if()
    //     // Destroy(player.gameObject);
    // }
    // public void HandleSplit(Interaction interaction)
    // {
    //     BeforeSplit?.Invoke(interaction.actor);

    //     // this.InitPlayer()
    //     AfterSplit?.Invoke(interaction.actor);
    // }
    // public bool IsCurrentCharacterSplited()
    // {
    //     return GetCurrentCharacterData().IS_SPLIT_REALITY_ORIGIN;
    // }
    // public CharacterData GetCurrentCharacterData()
    // {
    //     if (_playableCharacter != null)
    //     {
    //         return _playableCharacter[_currentPlayableCharacter];
    //     }
    //     return null;
    // }
    // public Player GetCurrentPlayer()
    // {
    //     Player player = null;
    //     for (int i = 0; i < this.gameObject.transform.childCount; i++)
    //     {
    //         GameObject current = this.gameObject.transform.GetChild(i).gameObject;
    //         // GameObject object = this.gameObject.transform.GetChild(i).gameObject;
    //         Player p = current.GetComponent<Player>();
    //         if (p.GetCharacterData().NAME == GetCurrentCharacterData().NAME)
    //         {
    //             player = p;
    //         }
    //     }
    //     return player;
    //     // return this.gameObject.transform.GetChild(_currentPlayableCharacter).gameObject.GetComponent<Player>();
    // }
    // public void CreateCharacter(CharacterData character, Transform spawn)
    // {
    //     //     // if (AreChildsExisting())
    //     //     // {
    //     //     //     BeforeCreateCharacter?.Invoke(GetCurrentPlayer());
    //     //     // }
    //     //     // if (character != null)
    //     //     // {
    //     //     //     if (this.gameObject.transform.childCount > 0)
    //     //     //     {
    //     //     //         for (int i = 0; i < this.gameObject.transform.childCount; i++)
    //     //     //         {
    //     //     //             Player player = this.gameObject.transform.GetChild(i).gameObject.GetComponent<Player>();
    //     //     //             if (player && player.GetName() != character.name)
    //     //     //             {
    //     //     //                 GameObject instantiation = Instantiate(character.PREFAB, spawn.position, character.PREFAB.transform.rotation, this.gameObject.transform);
    //     //     //                 player = instantiation.AddComponent<Player>();
    //     //     //                 player.Init(character);
    //     //     //             }
    //     //     //             // else if(player && player.GetName())

    //     //     //             // GameObject player = this.gameObject.transform.GetChild(i).gameObject;
    //     //     //             // if(player.name != character.PREFAB.name){
    //     //     //             //     GameObject instantiation = Instantiate(character.PREFAB,spawn,,character.PREFAB.transform.rotation,this.gameObject);
    //     //     //             //     instantiation.AddComponent<Player>();
    //     //     //             // }

    //     //     //         }
    //     //     //     }
    //     //     // }
    //     //     // AfterCreateCharacter?.Invoke(GetCurrentPlayer());
    // }
    // // public void ReconstructRecord()
    // // {

    // // }
    // // public void InitPlayer(CharacterData data)
    // // {
    // //     // _characterData = data;

    // // }
    // // public void Ref(GameObject cam)
    // // {
    // //     // _root = root;
    // //     _cam = cam;
    // // }
    // // public CharacterData GetCharacterData()
    // // {
    // //     return _characterData;
    // // }
    // public bool AreChildsExisting()
    // {
    //     return this.gameObject.transform.childCount > 0;
    // }
}
