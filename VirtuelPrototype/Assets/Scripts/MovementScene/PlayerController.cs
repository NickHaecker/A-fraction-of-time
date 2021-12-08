using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : Controller
{
    [SerializeField]
    private GameObject _playerRoot = null;
    [SerializeField]
    private Transform _spawnPoint = null;
    [SerializeField]
    private Player _currentCharacter = null;
    [SerializeField]
    private List<CharacterData> _playableCharacter = null;
    [SerializeField]
    private string _currentSelection = "";
    [SerializeField]
    private Player _temporalOldPlayer = null;

    public Action<Player> BeforeCharacterCreated;
    public Action<Player> AfterCharacterCreated;
    public Action<Player> CreateShadow;

    public void HandleStart()
    {
        CharacterData characterData = _playableCharacter[0];
        GameObject character = InstantiateCharacter(characterData.PREFAB);
        Player player = character.AddComponent<Player>();
        player.Init(characterData);
        _currentCharacter = player;
        AfterCharacterCreated?.Invoke(player);
    }
    private GameObject InstantiateCharacter(GameObject prefab)
    {
        GameObject character = null;
        if (prefab)
        {
            character = Instantiate(prefab, _spawnPoint.position, prefab.transform.rotation, this.gameObject.transform);
        }
        return character;
    }
    private void HandleInteractionListener(Interaction interaction)
    {
        _currentCharacter.InertInteractions(interaction);
    }
    public void HandleCharacterSelection(String name)
    {
        _currentSelection = name;
        // Debug.Log(_currentSelection);
        if (IsMerge())
        {
            HandleMerge();
        }
        else
        {
            HandleSplit();
        }
    }
    private void Update()
    {
        HandleInput();
    }
    private void ChangeCurrentCharacter(Player player)
    {
        _currentCharacter = player;
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SplitSelectionController splitSelectionController = this.gameObject.GetComponent<SplitSelectionController>();
            splitSelectionController.InitCharacterSelection(_playableCharacter, _currentCharacter);
        }
    }
    private void AddInteractionsListener(Player player)
    {
        Debug.Log(player);
        foreach (Ability ability in player.gameObject.GetComponents<Ability>())
        {
            ability.SubmitInteraction += HandleInteractionListener;
        }
    }
    private void RemoveInteractionsListener(Player player)
    {
        foreach (Ability ability in player.gameObject.GetComponents<Ability>())
        {
            ability.SubmitInteraction -= HandleInteractionListener;
        }
    }
    private void HandleSplit()
    {
        BeforeCharacterCreated?.Invoke(_currentCharacter);
        ChangeSpawnPoint(_currentCharacter);
        Destroy(_currentCharacter.gameObject);
        CharacterData newCharacter = GetCharacterData(_currentSelection);
        if(newCharacter)
        {
            GameObject playerObject = InstantiateCharacter(newCharacter.PREFAB);
            Player player = playerObject.AddComponent<Player>();
            player.Init(newCharacter);
            AfterCharacterCreated?.Invoke(player);
        }
        GameObject shadow = InstantiateCharacter(_temporalOldPlayer.GetCharacterData().PREFAB_GHOST);
        _temporalOldPlayer = null;
        shadow.transform.position = _currentCharacter.transform.position;


    }

    private CharacterData GetCharacterData(String uid)
    {
        CharacterData data = null;
        foreach(CharacterData c in _playableCharacter)
        {
            if(c.NAME == uid)
            {
                data = c;
            }
        }
        return data;
    }
    private void ChangeSpawnPoint(Player player)
    {
        Transform transform = player.gameObject.transform;
        transform.localScale = new Vector3(1,1,1);
        transform.rotation = new Quaternion();
        _spawnPoint = transform;
        //Destroy(player.gameObject);


    }
    private void OnCreateShadow()
    {

    }
    private void HandleTemporalOldPlayerSave(Player player)
    {
        _temporalOldPlayer = player;
    }
    private void HandleMerge()
    {

    }

    private void Start()
    {
        BeforeCharacterCreated += HandleTemporalOldPlayerSave;
        BeforeCharacterCreated += StateManager.SavePlayer;
        BeforeCharacterCreated += RemoveInteractionsListener;
        AfterCharacterCreated += ChangeCurrentCharacter;
        AfterCharacterCreated += AddInteractionsListener;
        if (_playerRoot == null)
        {
            _playerRoot = this.gameObject;
        }
        SplitSelectionController splitSelectionController = this.gameObject.GetComponent<SplitSelectionController>();
        if (splitSelectionController)
        {
            splitSelectionController.SelectCharacter += HandleCharacterSelection;
        }

    }

    private bool IsMerge()
    {
        bool isM = false;
        if (_currentCharacter != null)
        {
            int currentIndex = 0;
            foreach (CharacterData data in _playableCharacter)
            {
                if (data.NAME == _currentCharacter.GetCharacterData().NAME)
                {
                    break;
                }
                currentIndex++;
            }
            int potencialIndex = 0;
            foreach (CharacterData d in _playableCharacter)
            {
                if (d.NAME == _currentSelection)
                {
                    break;
                }
                potencialIndex++;
            }
            isM = potencialIndex < currentIndex;
        }
        return isM;
    }
}
