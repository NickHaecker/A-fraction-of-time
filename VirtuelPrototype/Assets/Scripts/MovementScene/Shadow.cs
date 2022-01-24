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
                case InteractionType.WALK | InteractionType.JUMP:

                    Vector3 positionVector = new Vector3(interaction.Target.Position[0],interaction.Target.Position[1],interaction.Target.Position[2]);
                    Vector3 moveVector = new Vector3(positionVector.x - transform.position.x,0,positionVector.z - transform.position.z);

                    transform.forward = moveVector;

                    transform.position = positionVector;

                    transform.localScale = new Vector3(interaction.Target.Scale[0],interaction.Target.Scale[1],interaction.Target.Scale[2]);
                    
                    gameObject.GetComponent<AnimationHandler>().SetGhostPosition(positionVector);

                    break;
                case InteractionType.JUMPSTART:
                    MoveShadow(interaction.Source);
                    gameObject.GetComponent<AnimationHandler>().jump();
                    break;
                case InteractionType.JUMPSTOP:
                    MoveShadow(interaction.Source);
                    gameObject.GetComponent<AnimationHandler>().stopJump();
                    Debug.Log("stopped jumping reconstructed!");
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

    private void MoveShadow(GameObjectSaveData source)
    {
        Vector3 positionVector = new Vector3(source.Position[0], source.Position[1], source.Position[2]);
        //transform.LookAt()
        GameObject newTransfrom = new GameObject();
        newTransfrom.hideFlags = HideFlags.HideInHierarchy;
        newTransfrom.transform.position = positionVector;
        newTransfrom.transform.rotation = Quaternion.Euler(new Vector3(source.Rotation[0], source.Rotation[1], source.Rotation[2]));
        transform.LookAt(newTransfrom.transform);
        transform.position = positionVector;
        transform.rotation = Quaternion.Euler(new Vector3(source.Rotation[0], source.Rotation[1], source.Rotation[2]));
        transform.localScale = new Vector3(source.Scale[0], source.Scale[1], source.Scale[2]);
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
