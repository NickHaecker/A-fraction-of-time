using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class Shadow : Player
{
    [SerializeField]
    private bool _isReconstructing = false;

    private InteractionSaveData _lastInteraction = null;

    public Action DestroyShadow;

    public void ReconstructRecord(float timestamp)
    {
        if(_interactions.Count > 0)
        {
            _lastInteraction  = _interactions.OrderByDescending(item => item.TimeStamp).First();
        }
        InteractionSaveData interaction = _interactions.Find(i => (_lastTimestamp < i.TimeStamp && i.TimeStamp <= timestamp));

        SetLastTimestamp(timestamp);

        if(interaction != null)
        {
            InteractionType type = (InteractionType)Enum.Parse(typeof(InteractionType),interaction.Type);
            switch(type)
            {
                case InteractionType.WALK:

                    Vector3 positionVector = new Vector3(interaction.Source.Position[0],interaction.Source.Position[1],interaction.Source.Position[2]);
                    //transform.LookAt()
                    GameObject newTransfrom = new GameObject();
                    newTransfrom.hideFlags = HideFlags.HideInHierarchy;
                    newTransfrom.transform.position = positionVector;
                    newTransfrom.transform.rotation = Quaternion.Euler(new Vector3(interaction.Source.Rotation[0],interaction.Source.Rotation[1],interaction.Source.Rotation[2]));
                    transform.LookAt(newTransfrom.transform);
                    transform.position = positionVector;
                    transform.rotation = Quaternion.Euler(new Vector3(interaction.Source.Rotation[0],interaction.Source.Rotation[1],interaction.Source.Rotation[2]));
                    transform.localScale = new Vector3(interaction.Source.Scale[0],interaction.Source.Scale[1],interaction.Source.Scale[2]);
                    gameObject.GetComponent<AnimationHandler>().SetGhostPosition(positionVector);

                    break;
                default:
                    break;
            }
        }
        if(_lastInteraction != null && _lastInteraction.TimeStamp < timestamp)
        {
            Destroy(this.gameObject);
            DestroyShadow?.Invoke();
        }
    }
    public void SetLastTimestamp(float timestamp)
    {
        _lastTimestamp = timestamp;
    }

    public void StartShadowing(bool state)
    {
        _isReconstructing = state;
    }
}
