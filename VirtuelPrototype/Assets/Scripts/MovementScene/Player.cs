using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Player : MonoBehaviour
{

    [SerializeField]
    protected CharacterData _data = null;
    [SerializeField]
    protected List<InteractionSaveData> _interactions;
    [SerializeField]
    protected float _lastTimestamp = 0f;




    public string GetName()
    {
        return _data.NAME;
    }
    public void Init(CharacterData data)
    {
        _data = data;
        _interactions = new List<InteractionSaveData>();
    }

    public void InsertInteraction(InteractionSaveData interaction)
    {
        _interactions.Add(interaction);
    }

    public void InsertInteractions(List<InteractionSaveData> interactions)
    {
        _interactions = interactions;
    }
    public CharacterData GetCharacterData()
    {
        return _data;
    }
    public void Delete()
    {
        Destroy(this.gameObject);
    }
    public List<InteractionSaveData> GetInteractions()
    {
        return _interactions;
    }

}
