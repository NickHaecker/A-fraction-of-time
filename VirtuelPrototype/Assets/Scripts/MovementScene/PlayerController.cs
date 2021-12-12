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
            character = Instantiate(prefab, FindNewSpawnPoint(_spawnPoint), prefab.transform.rotation, this.gameObject.transform);
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
        if (newCharacter)
        {
            RemoveShadow(newCharacter);
            GameObject playerObject = InstantiateCharacter(newCharacter.PREFAB);
            Player player = playerObject.AddComponent<Player>();
            player.Init(newCharacter);
            AfterCharacterCreated?.Invoke(player);
        }
        GameObject shadow = InstantiateCharacter(_temporalOldPlayer.GetCharacterData().PREFAB_GHOST);
        _temporalOldPlayer = null;



    }
    private void RemoveShadow(CharacterData character)
    {
        int count = this.gameObject.transform.childCount;
        //CharacterData possShad
        for (int i = 0; i < count; i++)
        {
            //if()
            GameObject gO = this.gameObject.transform.GetChild(i).gameObject;
            if (gO.name.Contains(character.PREFAB_GHOST.name))
            {
                Destroy(gO);
            }
        }
    }
    private CharacterData GetCharacterData(String uid)
    {
        CharacterData data = null;
        foreach (CharacterData c in _playableCharacter)
        {
            if (c.NAME == uid)
            {
                data = c;
            }
        }
        return data;
    }
    private void ChangeSpawnPoint(Player player)
    {

        Transform transform = player.gameObject.transform;
        transform.localScale = new Vector3(1, 1, 1);
        transform.rotation = new Quaternion();
        _spawnPoint = transform;

    }
    private Vector3 FindNewSpawnPoint(Transform transform)
    {
        Vector3 rayStart = transform.position + Vector3.up;
        Vector2 randomVector = UnityEngine.Random.insideUnitCircle;
        randomVector.Normalize();
        RaycastHit hit;
        while (Physics.Raycast(rayStart, new Vector3(randomVector.x, 0, randomVector.y), out hit, 1))
        {
            randomVector = UnityEngine.Random.insideUnitCircle;
            randomVector.Normalize();
        }
        return transform.position + new Vector3(randomVector.x, 0, randomVector.y);
    }
    //private void OnCreateShadow()
    //{

    //}
    private void ClearChildren()
    {

        int children = this.gameObject.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            GameObject child = this.gameObject.transform.GetChild(i).gameObject;
            Destroy(child);
        }
        Debug.Log("deleted all children");
    }
    private void HandleTemporalOldPlayerSave(Player player)
    {
        _temporalOldPlayer = player;
    }
    private void HandleMerge()
    {
        BeforeCharacterCreated?.Invoke(_currentCharacter);
        Debug.Log("start merge");
        Debug.Log(_currentSelection);
        SavePlayerData player = StateManager.LoadPlayer(_currentSelection);
        CharacterData playerData = GetCharacterData(_currentSelection);
        GameObject newSpawn = new GameObject("pc-157");
        newSpawn.hideFlags = HideFlags.HideInHierarchy;

        float[] position = player.Position.Position;
        float[] rotation = player.Position.Rotation;
        newSpawn.transform.position = new Vector3(position[0], position[1], position[2]);
        newSpawn.transform.rotation = Quaternion.Euler(new Vector3(rotation[0], rotation[1], rotation[2]));
        newSpawn.transform.localScale = new Vector3(1, 1, 1);
        _spawnPoint = newSpawn.transform;
        SavePlayerData shadow = StateManager.LoadPlayer(_temporalOldPlayer.GetName());
        CharacterData shadowData = GetCharacterData(shadow.Name);
        ClearChildren();

        GameObject newPlayer = InstantiateCharacter(playerData.PREFAB);
        Player newPlayerScript = newPlayer.AddComponent<Player>();
        newPlayerScript.Init(playerData);

        GameObject newShadow = InstantiateCharacter(shadowData.PREFAB_GHOST);
        Player newShadowPlayerScript = newShadow.AddComponent<Player>();
        newShadowPlayerScript.Init(shadowData);
        newShadowPlayerScript.ReconstructRecord(Utils.ConvertInteractions(shadow.Interactions, _playableCharacter));

        AfterCharacterCreated?.Invoke(newPlayerScript);
        _temporalOldPlayer = null;

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
