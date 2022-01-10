using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Shadow : Player
{
    [SerializeField]
    private bool _isReconstructing = false;


    public Action<GameObject> DestroyShadow;

    public void ReconstructRecord(float timestamp)
    {
        InteractionSaveData interaction = _interactions.Find(i => (_lastTimestamp < i.TimeStamp && i.TimeStamp <= timestamp));

        SetLastTimestamp(timestamp);

        if (interaction != null)
        {
            InteractionType type = (InteractionType)Enum.Parse(typeof(InteractionType), interaction.Type);
            switch (type)
            {
                case InteractionType.WALK:

                    transform.position = new Vector3(interaction.Source.Position[0], interaction.Source.Position[1], interaction.Source.Position[2]);
                    transform.rotation = Quaternion.Euler(new Vector3(interaction.Source.Rotation[0], interaction.Source.Rotation[1], interaction.Source.Rotation[2]));
                    transform.localScale = new Vector3(interaction.Source.Scale[0], interaction.Source.Scale[1], interaction.Source.Scale[2]);

                    break;
                default:
                    break;
            }
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
