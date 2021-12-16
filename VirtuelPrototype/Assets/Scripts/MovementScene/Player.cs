using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Player : MonoBehaviour
{

    [SerializeField]
    private CharacterData _data = null;
    [SerializeField]
    private List<InteractionSaveData> _interactions;
    [SerializeField]
    private bool _isReconstructing = false;
    [SerializeField]
    private float _lastTimestamp = 0f;

    public Action<GameObject> DestroyShadow;



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
    public void ReconstructRecord(float timestamp)
    {

        if(timestamp < _lastTimestamp)
        {
            InteractionSaveData interaction = _interactions.Find(i => (_lastTimestamp < i.TimeStamp && i.TimeStamp <= timestamp));

            //SetLastTimestamp(timestamp);


            if(interaction != null)
            {
                InteractionType type = (InteractionType)Enum.Parse(typeof(InteractionType),interaction.Type);
                switch(type)
                {
                    case InteractionType.WALK:

                        transform.position = new Vector3(interaction.Source.Position[0],interaction.Source.Position[1],interaction.Source.Position[2]);
                        transform.rotation = Quaternion.Euler(new Vector3(interaction.Source.Rotation[0],interaction.Source.Rotation[1],interaction.Source.Rotation[2]));
                        transform.localScale = new Vector3(interaction.Source.Scale[0],interaction.Source.Scale[1],interaction.Source.Scale[2]);

                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            DestroyShadow?.Invoke(this.gameObject);
        }
     

       

    }
    public void SetLastTimestamp(float timestamp)
    {
        
        _lastTimestamp = timestamp;
    }

    public void InsertInteractions(List<InteractionSaveData> interactions)
    {
        _interactions = interactions;
        _lastTimestamp = _interactions[_interactions.Count - 1].TimeStamp;
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
    public void StartShadowing(bool state)
    {
        _isReconstructing = state;
    }
}
