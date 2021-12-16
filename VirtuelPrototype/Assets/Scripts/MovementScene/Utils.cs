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
    //public static List<Interaction> ConvertInteractions(List<InteractionSaveData> savedInteractions, List<CharacterData> data)
    //{



    //    List<Interaction> interactions = new List<Interaction>();
    //    Debug.Log(savedInteractions);
    //    foreach(InteractionSaveData interaction in savedInteractions)
    //    {

    //        InteractionType type = (InteractionType)Enum.Parse(typeof(InteractionType),interaction.Type);
    //        Player actor = new GameObject().AddComponent<Player>();
    //        actor.gameObject.hideFlags = HideFlags.HideInHierarchy;

    //        actor.Init(GetCharacterData(data,interaction.Actor));
    //        GameObject interactedObject = new GameObject();
    //        interactedObject.hideFlags = HideFlags.HideInHierarchy;

    //        //Debug.Log(interactedObject.name);
    //        float[] position = interaction.Target.Position;
    //        float[] scale = interaction.Target.Scale;
    //        float[] rotation = interaction.Target.Rotation;

    //        interactedObject.transform.position = new Vector3(position[0],position[1],position[2]);
    //        interactedObject.transform.rotation = Quaternion.Euler(new Vector3(rotation[0],rotation[1],rotation[2]));
    //        interactedObject.transform.localScale = new Vector3(scale[0],scale[1],scale[2]);

    //        interactedObject.name = interaction.Target.Name;
    //        interactedObject.layer = interaction.Target.Layer;
    //        interactedObject.tag = interaction.Target.Tag;

    //        Transform interactedPosition = new GameObject().transform;
    //        interactedPosition.gameObject.hideFlags = HideFlags.HideInHierarchy;
    //        //Debug.Log(interactedPosition.gameObject.name);
    //        float[] iposition = interaction.Source.Position;
    //        float[] iscale = interaction.Source.Scale;
    //        float[] irotation = interaction.Source.Rotation;

    //        interactedPosition.position = new Vector3(iposition[0],iposition[1],iposition[2]);
    //        interactedPosition.localScale = new Vector3(iscale[0],iscale[1],iscale[2]);
    //        interactedPosition.rotation = Quaternion.Euler(new Vector3(irotation[0],irotation[1],irotation[2]));

    //        float time = interaction.TimeStamp;

    //        Interaction i = new Interaction(type,actor,interactedObject,interactedPosition,time);

    //        interactions.Add(i);



    //    }
    //    return interactions;
    //}

    //private static CharacterData GetCharacterData(List<CharacterData> data, string whichOne)
    //{
    //    CharacterData cD = null;
    //    foreach(CharacterData d in data)
    //    {
    //        if(d.NAME == whichOne)
    //        {
    //            cD = d;
    //        }
    //    }
    //    return cD;
    //}
}
