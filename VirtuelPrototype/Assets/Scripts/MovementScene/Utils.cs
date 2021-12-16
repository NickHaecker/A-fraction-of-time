using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public static class Utils
{
    public static InteractionSaveData GetInteractionSaveState(Interaction interaction)
    {
        InteractionSaveData interactionSaveData = new InteractionSaveData();
        interactionSaveData.Type = interaction.type.ToString();
        interactionSaveData.Actor = interaction.actor.GetCharacterData().name;

        GameObjectSaveData source = new GameObjectSaveData();
        source.Name = interaction.interactedObject.name;
        source.Tag = interaction.interactedObject.tag;
        source.Layer = interaction.interactedObject.layer;

        source.Position[0] = interaction.interactedObject.gameObject.transform.position.x;
        source.Position[1] = interaction.interactedObject.gameObject.transform.position.y;
        source.Position[2] = interaction.interactedObject.gameObject.transform.position.z;

        source.Scale[0] = interaction.interactedObject.gameObject.transform.localScale.x;
        source.Scale[1] = interaction.interactedObject.gameObject.transform.localScale.y;
        source.Scale[2] = interaction.interactedObject.gameObject.transform.localScale.z;

        source.Rotation[0] = interaction.interactedObject.gameObject.transform.rotation.x;
        source.Rotation[1] = interaction.interactedObject.gameObject.transform.rotation.y;
        source.Rotation[2] = interaction.interactedObject.gameObject.transform.rotation.z;
        interactionSaveData.Source = source;
        GameObjectSaveData target = new GameObjectSaveData();
        target.Name = interaction.interactionPosition.gameObject.name;
        target.Tag = interaction.interactionPosition.gameObject.tag;
        target.Layer = interaction.interactionPosition.gameObject.layer;

        target.Position[0] = interaction.interactionPosition.transform.position.x;
        target.Position[1] = interaction.interactionPosition.transform.position.y;
        target.Position[2] = interaction.interactionPosition.transform.position.z;

        target.Scale[0] = interaction.interactionPosition.transform.localScale.x;
        target.Scale[1] = interaction.interactionPosition.transform.localScale.y;
        target.Scale[2] = interaction.interactionPosition.transform.localScale.z;

        target.Rotation[0] = interaction.interactionPosition.transform.rotation.x;
        target.Rotation[1] = interaction.interactionPosition.transform.rotation.y;
        target.Rotation[2] = interaction.interactionPosition.transform.rotation.z;
        interactionSaveData.Target = target;

        interactionSaveData.TimeStamp = interaction.timestamp;
        return interactionSaveData;
    }
}
