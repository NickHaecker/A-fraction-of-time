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
                    MoveShadow(interaction.Target);
                    break;

                case InteractionType.JUMPSTART:
                    MoveShadow(interaction.Source);
                    gameObject.GetComponent<AnimationHandler>().jump();
                    break;

                case InteractionType.JUMPSTOP:
                    MoveShadow(interaction.Source);
                    gameObject.GetComponent<AnimationHandler>().stopJump();
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

    private void MoveShadow(GameObjectSaveData data)
    {
        Vector3 positionVector = new Vector3(data.Position[0], data.Position[1], data.Position[2]);
        Vector3 moveVector = new Vector3(positionVector.x - transform.position.x, 0, positionVector.z - transform.position.z);

        transform.forward = moveVector;

        transform.position = positionVector;

        transform.localScale = new Vector3(data.Scale[0], data.Scale[1], data.Scale[2]);

        gameObject.GetComponent<AnimationHandler>().SetGhostPosition(positionVector);

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
